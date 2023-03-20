namespace Spaceship.IoC.Test.No.Strategies;
using System.Collections.Concurrent;
using Moq;
using Hwdtech;
using Spaceship__Server;

public class Stateful
{
    [Fact]
    public object CreateIoCDependencies()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();

        Dictionary<string, MyThread> GameThreads = new();

        Dictionary<string, ISender> GameSenders = new();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get id by thread", (object[] args) => {
            MyThread thread = (MyThread)args[0];

            return GameThreads.FirstOrDefault(t => t.Value == thread).Key;
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get sender by id", (object[] args) => 
        {
            string id = (string)args[0];
            
            return GameSenders[id];
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get thread by id", (object[] args) => 
        {
            string id = (string)args[0];
            
            return GameThreads[id];
        }).Execute();
        
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create and Start Thread", (object[] args) => 
        {
            if (args.Count() == 2)
            {
                string id = (string)args[0]; 
                Action action = (Action)args[1];
                BlockingCollection<Spaceship__Server.ICommand> q = new();

                ISender sender = new SenderAdapter(q);
                IReciver receiver = new RecieverAdapter(q);
                MyThread thread = new(receiver);

                q.Add(new ActionCommand(action));

                thread.Start();

                GameThreads.Add(id, thread);
                GameSenders.Add(id, sender);

                return thread;
            }
            else{
                string id = (string)args[0]; 
                BlockingCollection<Spaceship__Server.ICommand> q = new();

                ISender sender = new SenderAdapter(q);
                IReciver receiver = new RecieverAdapter(q);
                MyThread thread = new(receiver);

                thread.Start();

                GameThreads.Add(id, thread);
                GameSenders.Add(id, sender);

                return thread;
            }
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Send Command", (object[] args) => 
        {
            string id = (string)args[0]; 

            ISender sender = IoC.Resolve<ISender>("Get sender by id", id);

            Spaceship__Server.ICommand cmd = (Spaceship__Server.ICommand)args[1];

            return sender.Send(cmd);
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Message deserialize", (object[] args) => {
            Spaceship__Server.ICommand cmd = (Spaceship__Server.ICommand) args[0];

            return cmd;
        }).Execute();     

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Hard Stop Thread", (object[] args) => 
        {
            ISender sender = IoC.Resolve<ISender>("Get sender by id", (string)args[0]);
            if (args.Count() == 2)
            {
                string id = (string)args[0]; 
                Action action = (Action)args[1];

                MyThread thread = IoC.Resolve<MyThread>("Get thread by id", id);
                BCPushCommand send = new BCPushCommand(((SenderAdapter)sender).queue, new List<Spaceship__Server.ICommand>(){
                    new UpdateBehaviourCommand(thread, thread.strategy += action),
                    new HardStopCommand(IoC.Resolve<MyThread>("Get thread by id", id))});

                return send;
            }
            else{
                string id = (string)args[0]; 

                MyThread thread = IoC.Resolve<MyThread>("Get thread by id", id);
                BCPushCommand send = new BCPushCommand(((SenderAdapter)sender).queue, new List<Spaceship__Server.ICommand>(){
                    new HardStopCommand(IoC.Resolve<MyThread>("Get thread by id", id))});

                return send;
            }
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Soft Stop Thread", (object[] args) => 
        {
            ISender sender = IoC.Resolve<ISender>("Get sender by id", (string)args[0]);
           if (args.Count() == 2)
            {
                string id = (string)args[0]; 
                Action action = (Action)args[1];

                MyThread thread = IoC.Resolve<MyThread>("Get thread by id", id);
                BCPushCommand send = new BCPushCommand(((SenderAdapter)sender).queue, new List<Spaceship__Server.ICommand>(){
                    new UpdateBehaviourCommand(thread, thread.strategy += action),
                    new SoftStopCommand(IoC.Resolve<MyThread>("Get thread by id", id))});

                return send;
            }
            else{
                string id = (string)args[0]; 

                MyThread thread = IoC.Resolve<MyThread>("Get thread by id", id);
                BCPushCommand send = new BCPushCommand(((SenderAdapter)sender).queue, new List<Spaceship__Server.ICommand>(){
                    new SoftStopCommand(IoC.Resolve<MyThread>("Get thread by id", id))});

                return send;
            }
        }).Execute();

        return scope;
    }

    [Fact]
    public void MyThreadCreationUsengRecieverAdapter()
    {
        Action action = () => {};
        BlockingCollection<Spaceship__Server.ICommand> q = new();

        ISender sender = new SenderAdapter(q);
        IReciver receiver = new RecieverAdapter(q);
        MyThread thread = new(receiver);

        sender.Send(new ActionCommand(action));

        thread.Start();

        Thread.Sleep(300);

        Assert.Empty(q);
    }

    [Fact]
    public void WrongThreadStop()
    {
        CreateIoCDependencies();

        BlockingCollection<Spaceship__Server.ICommand> q = new();
        BlockingCollection<Spaceship__Server.ICommand> q1 = new();


        IReciver receiver = new RecieverAdapter(q);
        IReciver receiver1 = new RecieverAdapter(q);
        MyThread thread = new(receiver);
        MyThread wrongthread = new(receiver1);

        Action action = () => {
            Assert.Throws<Exception>(() => {
            new HardStopCommand(wrongthread).Execute();
            });
        };

        q.Add(new ActionCommand(action));

        thread.Start();

        Thread.Sleep(500);
    }
    
    [Fact]
    public void RecieverAdapterTests()
    {
        BlockingCollection<Spaceship__Server.ICommand> q = new();

        Mock<Spaceship__Server.ICommand> cmd = new();

        q.Add(cmd.Object);

        IReciver rec = new RecieverAdapter(q);

        Assert.Equal(cmd.Object, rec.Receive());

        Assert.True(rec.isEmpty());
    }

    [Fact]
    public void SenderAdapterTests()
    {
        BlockingCollection<Spaceship__Server.ICommand> q = new();

        Mock<Spaceship__Server.ICommand> cmd = new();

        ISender rec = new SenderAdapter(q);

        Assert.Empty(q);

        rec.Send(cmd.Object).Execute();

        Assert.Single(q);

    }

    [Fact]
    public void AdaptersFieldsTest()
    {
        BlockingCollection<Spaceship__Server.ICommand> q = new();

        RecieverAdapter rec = new RecieverAdapter(q);

        SenderAdapter snd = new SenderAdapter(q);

        Assert.Equal(q, snd.queue);

        Assert.Equal(q, rec.queue);
    }

    [Fact]
    public void HardCodedStopThread()
    {
        BlockingCollection<Spaceship__Server.ICommand> q = new();

        IReciver rec = new RecieverAdapter(q);

        MyThread thread = new(rec);

        thread.Start();

        q.Add(new ThreadStopCommand(thread));

        Thread.Sleep(1000);

        Assert.True(thread.stop);
    }
    [Fact]
    public void SendSingleCommandIntoLambdaInitializedThread()
    {
        CreateIoCDependencies();

        MyThread thread = IoC.Resolve<MyThread>("Create and Start Thread", "1", () => {});

        ActionCommand cmd =  new(() => {Thread.Sleep(1000);});

        IoC.Resolve<Spaceship__Server.ICommand>("Send Command", "1", cmd).Execute();

        cmd =  new ActionCommand (() => {});

        IoC.Resolve<Spaceship__Server.ICommand>("Send Command", "1", cmd).Execute();

        Thread.Sleep(100);

        Assert.Single(((RecieverAdapter)thread.receiver).queue);

        Thread.Sleep(1500);

        Assert.Empty(((RecieverAdapter)thread.receiver).queue);

    }

    [Fact]
    public void SendSingleCommandIntoLambdaLessInitializedThread()
    {
        CreateIoCDependencies();

        MyThread thread = IoC.Resolve<MyThread>("Create and Start Thread", "1");

        ActionCommand cmd =  new(() => {Thread.Sleep(1000);});

        IoC.Resolve<Spaceship__Server.ICommand>("Send Command", "1", cmd).Execute();

        cmd =  new ActionCommand (() => {});

        IoC.Resolve<Spaceship__Server.ICommand>("Send Command", "1", cmd).Execute();

        Thread.Sleep(100);

        Assert.Single(((RecieverAdapter)thread.receiver).queue);

        Thread.Sleep(1500);

        Assert.Empty(((RecieverAdapter)thread.receiver).queue);
    }
    
    [Fact]
    public void SoftStopThreadLambdaless()
    {
        object scope = CreateIoCDependencies();

        MyThread thread = IoC.Resolve<MyThread>("Create and Start Thread", "1", () => {IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();});

        IoC.Resolve<Spaceship__Server.ICommand>("Soft Stop Thread", "1").Execute();

        Thread.Sleep(1000);

        Assert.True(thread.stop);
    }

    [Fact]
    public void SoftStopThread()
    {
        object scope = CreateIoCDependencies();

        MyThread thread = IoC.Resolve<MyThread>("Create and Start Thread", "1", () => {IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();});

        IoC.Resolve<Spaceship__Server.ICommand>("Soft Stop Thread", "1", () => {}).Execute();

        Thread.Sleep(1000);

        Assert.True(thread.stop);
    }

    [Fact]
    public void HardStopThreadLambdaless()
    {
        CreateIoCDependencies();

        MyThread thread = IoC.Resolve<MyThread>("Create and Start Thread", "1");

        IoC.Resolve<Spaceship__Server.ICommand>("Hard Stop Thread", "1").Execute();

        Thread.Sleep(300);

        Assert.True(thread.stop);
    }

    [Fact]
    public void HardStopThread()
    {
        CreateIoCDependencies();

        MyThread thread = IoC.Resolve<MyThread>("Create and Start Thread", "1");

        IoC.Resolve<Spaceship__Server.ICommand>("Hard Stop Thread", "1", () => {}).Execute();

        Thread.Sleep(300);

        Assert.True(thread.stop);
    }
}