using Hwdtech;
using Moq;
using Spaceship__Server;

namespace Spaceship.Macro.Test;

public class MacroBuilderTest
{
    [Fact]
    public void InitialTest()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IUObject.MoveCommand", (object[] args) => 
        {

            MovableAdapter adp = new MovableAdapter(args);
            return adp;
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartMoveCommand.Get.Dependencies", (object[] args) =>
        {
            List<string> deps = new List<string>{"Object", "Velocity", "Queue"};
            return deps;
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartMoveCommand", (object[] args) =>
        {
            IoCStartMoveCommand creator = new();
            return creator.CreateStartMoveCommand(args);
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IoC.CreateMacro", (object[] args) =>
        {
            MacroCreator creator = new();
            return creator.CreateMacro(args);
        }).Execute();

        Mock<IUObject> obj = new();

        Mock<IUObject> _obj = new();

        Queue<Spaceship__Server.ICommand> _queue = new();

        _obj.Setup(o => o.get_property("Velocity")).Returns((object) new Vector(1, 1));

        _obj.Setup(o => o.get_property("Position")).Returns((object) new Vector(0, 0));

        obj.Setup(o => o.get_property("Object")).Returns((object) _obj.Object);

        obj.Setup(o => o.get_property("Queue")).Returns((object) _queue);

        obj.Setup(o => o.get_property("Velocity")).Returns((object) new Vector(5, 5) );

        IoC.Resolve<IMacro>("IoC.CreateMacro", "StartMoveCommand", obj.Object).Execute();

        Assert.Equal(2, _queue.Count);
    }
}