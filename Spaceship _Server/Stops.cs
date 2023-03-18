using Hwdtech;

namespace Spaceship__Server;
using System;
using Hwdtech;

public class HardStopCommand : ICommand
{
    MyThread thread;
    public HardStopCommand(MyThread threadtostop)
    {
        this.thread = threadtostop;
    }

    public void Execute()
    {
        new ThreadStopCommand(thread).Execute();
    }
}

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

        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Send Command", id, new HardStopCommand(thread)).Execute();
    }
}