using System;
using Spaceship__Server;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.IoC.Test.No.Strategies
{
    public class EndMoveCommandTest
    {
        [Fact]
        public void EndMoveCommand()
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IUObject.IMovable", (object[] args) =>
            {
                MovableAdapter adp = new MovableAdapter(args);
                return adp;
            }).Execute();

            Mock<IUObject> order = new();

            Mock<IUObject> _obj = new();

            Queue<Spaceship__Server.ICommand> _queue = new();

            _obj.Setup(o => o.get_property("Velocity")).Returns((object)new Vector(1, 1));

            _obj.Setup(o => o.get_property("Position")).Returns((object)new Vector(0, 0));

            order.Setup(o => o.get_property("Object")).Returns((object)_obj.Object);

            order.Setup(o => o.get_property("Queue")).Returns((object)_queue);

            order.Setup(o => o.get_property("Velocity")).Returns((object)new Vector(5, 5));

            StartMoveCommand cmd = new(order.Object);

            BridgeCommand bridge = new(cmd);

            bridge.Execute();

            Assert.Equal(2, _queue.Count);

            Mock<IUObject> writ = new();

            writ.Setup(o => o.get_property("Object")).Returns(_obj.Object);

            writ.Setup(o => o.get_property("Command")).Returns(bridge);

            EndMoveCommand endcmd = new(writ.Object);

            endcmd.Execute();

            Assert.Equal(2, _queue.Count);

            Console.WriteLine(_queue.Dequeue().GetType());
            Console.WriteLine(_queue.Dequeue().GetType());
        }
    }
}
