using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hwdtech;
namespace Spaceship__Server;


public interface IReciver
{
    ICommand Receive();
    bool isEmpty();
}

public class MyThread
{
    public Thread thread;
    public IReciver receiver;
    bool stop = false;
    public Action strategy;

    internal void Stop() => stop = true;

    internal void HandleCommand()
    {
        var cmd = receiver.Receive();

        cmd.Execute();
    }
    public MyThread(IReciver queue)
    {
        this.receiver = queue;
        strategy = () =>
        {
            HandleCommand();
        };

        thread = new Thread(() =>
        {
            while (!stop) strategy();
        });
    }
    internal void UpdateBehaviour(Action newBehaviour)
    {
        strategy = newBehaviour;

    }
    public void Start()
    {
        thread.Start();
    }
}

public interface ISender
{
    Spaceship__Server.ICommand Send(object message);
    //sender.setup(s => s.Send(It.IsAny<ICommand>())).Callback<ICommand>((command) => q.send())
}

public class UpdateBehaviourCommand : Spaceship__Server.ICommand
{
    Action behaviour;
    MyThread thread;

    public UpdateBehaviourCommand(MyThread thread, Action newBehaviour)
    {
        this.behaviour = newBehaviour;
        this.thread = thread;
    }
    public void Execute()
    {
        thread.UpdateBehaviour(this.behaviour);
    }
}

public class ThreadStopCommand : Spaceship__Server.ICommand
{
    MyThread stoppingThread;
    public ThreadStopCommand(MyThread stoppingThread) => this.stoppingThread = stoppingThread;

    public void Execute()
    {
        if (Thread.CurrentThread == stoppingThread.thread)
        {
            stoppingThread.Stop();
        }
        else
        {
            throw new Exception();
        }
    }
}

public class RecieverAdapter : IReciver
{
    public BlockingCollection<Spaceship__Server.ICommand> queue;

    public RecieverAdapter(BlockingCollection<Spaceship__Server.ICommand> queue) => this.queue = queue;

    public Spaceship__Server.ICommand Receive()
    {
        return queue.Take();
    }

    public bool isEmpty()
    {
        return queue.Count == 0;
    }
}

public class SenderAdapter : ISender
{
    public BlockingCollection<Spaceship__Server.ICommand> queue;

    public SenderAdapter(BlockingCollection<Spaceship__Server.ICommand> queue) => this.queue = queue;

    public Spaceship__Server.ICommand Send(object msg)
    {
        Spaceship__Server.ICommand cmd = IoC.Resolve<Spaceship__Server.ICommand>("Message deserialize", msg);
        
        BCPushCommand pusher = new(queue, new List<Spaceship__Server.ICommand>(){cmd});

        return pusher;
    }
}
