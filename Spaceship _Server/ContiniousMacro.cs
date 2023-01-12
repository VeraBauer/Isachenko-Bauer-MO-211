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
            Spaceship__Server.ICommand mcmd= IoC.Resolve<Spaceship__Server.ICommand>("IoC.CraeteMacro", commandname, obj);
            macrocommand = new PushCommand(mcmd);
        }
    }
}
