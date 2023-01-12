using Hwdtech;
using System.Collections.Generic;

namespace Spaceship__Server
{
    public class PushCommand : ICommand
    {
        Spaceship__Server.ICommand comd = null;
        public PushCommand(Spaceship__Server.ICommand cmd)
        {
            comd = cmd;
        }
        public void Execute()
        {
            Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Enqueue(comd);
            Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Enqueue(this);
        }
    }
}
