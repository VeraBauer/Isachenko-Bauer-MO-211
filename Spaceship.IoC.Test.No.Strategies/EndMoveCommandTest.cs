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
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get.Queue", (object[] args) =>
            {
                return _queue;
            }).Execute();

            Mock<Spaceship__Server.ICommand> cmd = new();

            BridgeCommand bridge = new(cmd.Object);

            MacroCommand macro = new(IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Get.Queue"), new List<Spaceship__Server.ICommand> { bridge });

            macro.Execute();

            Assert.Equal(2, IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Get.Queue").Count);

            Assert.Equal(cmd.Object.GetType(), ((BridgeCommand)IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Get.Queue").Peek()).internalCommand.GetType());

            IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Get.Queue").Dequeue().Execute();

            IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Get.Queue").Dequeue().Execute();

            Mock<IUObject> writ = new();

            writ.Setup(o => o.get_property("Command")).Returns(bridge);

            Mock<IUObject> obj = new();

            obj.Setup(o => o.get_property("Velocity")).Returns(1);

            writ.Setup(o => o.get_property("Object")).Returns(obj.Object);

            EndMoveCommand end = new(writ.Object);

            end.Execute();

            Assert.Equal("Spaceship__Server.EmptyCommand", ((BridgeCommand)IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Get.Queue").Peek()).internalCommand.GetType().ToString());
        }
        [Fact]
        public void NothingTest()
        {
            EmptyCommand cmd = new();
            cmd.Execute();
        }
    }
}