using Spaceship__Server;
using Moq;

namespace Spaceship.IoC.Test.No.Strategies
{
    using Hwdtech;
    public class EndMoveCommandTest
    {
        [Fact]
        public void EndMoveCommand()
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
            Queue<Spaceship__Server.ICommand> _queue = new();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IoC.GetQueue", (object[] args) =>
            {
                return _queue;
            }).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "DeleteProperty", (object[] args) => {
                Mock<ICommand> DeletionCommand = new();
                DeletionCommand.Object.Execute();
                return (IUObject) args[0];
            }).Execute();

            Mock<Spaceship__Server.ICommand> cmd = new();

            BridgeCommand bridge = new(cmd.Object);

            PushCommand pusher = new(bridge);

            pusher.Execute();

            Assert.Equal(2, IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Count);

            Console.WriteLine(cmd.Object.GetType());
            Console.WriteLine(((BridgeCommand)IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Peek()).internalCommand.GetType());


            Assert.Equal(cmd.Object.GetType(), ((BridgeCommand)IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Peek()).internalCommand.GetType());

            IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Dequeue().Execute();

            IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Dequeue().Execute();

            Mock<IUObject> writ = new();

            Mock<IMoveCommandEndable> CmdToEnd= new();

            CmdToEnd.Setup(c => c.EndCommand).Returns(bridge);

            writ.Setup(o => o.get_property("Command")).Returns(CmdToEnd.Object);

            Mock<IUObject> obj = new();

            obj.Setup(o => o.get_property("Velocity")).Returns(1);

            writ.Setup(o => o.get_property("Object")).Returns(obj.Object);

            EndMoveCommand end = new(writ.Object);

            end.Execute();

            Assert.Equal("Spaceship__Server.EmptyCommand", ((BridgeCommand)IoC.Resolve<Queue<Spaceship__Server.ICommand>>("IoC.GetQueue").Peek()).internalCommand.GetType().ToString());
        }
        [Fact]
        public void NothingTest()
        {
            EmptyCommand cmd = new();
            cmd.Execute();
        }
    }
}
