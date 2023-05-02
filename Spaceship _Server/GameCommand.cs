using Hwdtech;
using System;
namespace Spaceship__Server;


public class GameCommand : Spaceship__Server.ICommand
{
    public object scope;
    public GameCommand(object scope)
    {
        this.scope = scope;
    }
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", this.scope).Execute();

        DateTime begin = DateTime.Now;

        while(DateTime.Now.Subtract(begin) < IoC.Resolve<TimeSpan>("Game.Current.Timespan"))
        {
            try
            {
            
                Spaceship__Server.ICommand cmd = IoC.Resolve<Spaceship__Server.ICommand>("Game.Current.HandleCommand");
                
                if (cmd != null)
                {
                    cmd.Execute();
                }
                else{
                    break;
                }
            }
            catch(Exception e)
            {
                IoC.Resolve<Spaceship__Server.ICommand>("HandleException", e, IoC.Resolve<string>("Get.Exception.Source", e)).Execute();
            }
        }
    }
}
