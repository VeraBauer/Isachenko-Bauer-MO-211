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
            MacroCommand mcmd= IoC.Resolve<MacroCommand>("IoC.CreateMacro", commandname, obj);
            mcmd._jobs.Add(new MacroCommand(Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue"), new List<ICommand>(){mcmd}));
            macrocommand = mcmd;
        }
    }
}
