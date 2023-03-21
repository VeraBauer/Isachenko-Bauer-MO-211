using Hwdtech;

namespace Spaceship__Server;
using System;
using Hwdtech;

public class SoftStopCommand : ICommand
{
    MyThread thread;

    public SoftStopCommand(MyThread threadtostop)
    {
        this.thread = threadtostop;
    }

    public void Execute()
    {
        string id = Hwdtech.IoC.Resolve<string>("Get id by thread", thread);

        new UpdateBehaviourCommand(thread, () => {
            while(!thread.receiver.isEmpty())
            {
                while(!thread.stop)
                {
                    thread.HandleCommand();
                }
            }
            Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Send Command", id, new HardStopCommand(thread)).Execute();

        }).Execute();
    }
}
