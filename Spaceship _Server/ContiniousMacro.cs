using Hwdtech;
using System.Collections.Generic;

namespace Spaceship__Server
{
    public class ContiniousMacro
    {
        public Spaceship__Server.ICommand macrocommand = null;
        public ContiniousMacro(object[] args)
        {
            string commandname = (string)args[0];
            IUObject obj = (IUObject)args[1];
            Spaceship__Server.ICommand mcmd= IoC.Resolve<Spaceship__Server.ICommand>("IoC.CreateMacro", commandname, obj);
            macrocommand = new MacroCommand(Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue"), 
            new List<ICommand>(){mcmd, new MacroCommand(Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue"), 
            new List<ICommand>(){mcmd})});
        }
    }
}
