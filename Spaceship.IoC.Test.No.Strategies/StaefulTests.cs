namespace Spaceship.IoC.Test.No.Strategies;
using System.Collections.Concurrent;
using Moq;
using Hwdtech;
using Spaceship__Server;

public class Stateful
{
    [Fact]
    public void InitialTest()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Dictionary<string, MyThread> GameThreads = new();

        ISender sender = new SenderAdapter(new BlockingCollection<Spaceship__Server.ICommand>());

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create and Start Thread", (object[] args) => 
        {
            if (args.Count() == 2)
            {
                string id = (string)args[0]; 
                Action action = (Action)args[1];
                BlockingCollection<Spaceship__Server.ICommand> q = new();

                sender = new SenderAdapter(q);
                IReciver receiver = new RecieverAdapter(q);
                MyThread thread = new(receiver);

                q.Add(new ActionCommand(action));

                thread.Start();

                GameThreads.Add(id, thread);

                return thread;
            }
            else{
                string id = (string)args[0]; 
                BlockingCollection<Spaceship__Server.ICommand> q = new();

                sender = new SenderAdapter(q);
                IReciver receiver = new RecieverAdapter(q);
                MyThread thread = new(receiver);

                thread.Start();

                GameThreads.Add(id, thread);

                return thread;
            }
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Send Command", (object[] args) => 
        {
            string id = (string)args[0]; 

            Spaceship__Server.ICommand cmd = (Spaceship__Server.ICommand)args[1];

            return sender.Send(cmd);
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Message deserialize", (object[] args) => {
            Spaceship__Server.ICommand cmd = (Spaceship__Server.ICommand) args[0];

            return cmd;
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get thread by id", (object[] args) => 
        {
            string id = (string)args[0];
            
            return GameThreads[id];
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Hard Stop Thread", (object[] args) => 
        {
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
    }
}