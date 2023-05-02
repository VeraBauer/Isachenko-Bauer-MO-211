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
                IoC.Resolve<Spaceship__Server.ICommand>("Game.Current.HandleCommand").Execute();
            }
            catch(Exception e)
            {
                IoC.Resolve<Spaceship__Server.ICommand>("HandleException", e, IoC.Resolve<string>("Get.Exception.Source", e)).Execute();
            }
        }
    }
}
