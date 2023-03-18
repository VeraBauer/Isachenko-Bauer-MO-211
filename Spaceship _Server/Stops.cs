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
        Console.WriteLine("Trying!");
        string id = Hwdtech.IoC.Resolve<string>("Get id by thread", thread);

        Console.WriteLine(id);

        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Send Command", "1", new HardStopCommand(thread)).Execute();
    }
}