using Hwdtech;
using Spaceship__Server;
using Spaceship__Server.Strategies;
using System;
using Moq;



namespace Spaceship.IoC.Tests
{
    public class UnitTest1
    {
        private Mock<IQueue> Moqueue = new();
        
        [Fact]
        public void ContiniousMovementTest()
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IQueue.Push", (object[] args) =>
            {
                return Moqueue.Object.Enqueue(args);
            }).Execute();

            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IUObject.IMovable.Continious", (object[] args) =>
            {
                MoveCommandContiniousAdapter mcc = new MoveCommandContiniousAdapter();
                return mcc.Continious(args);
            }).Execute();
            
            Mock<IUObject> Mobj = new();

            Mobj.Setup(o => o.get_property("Speed")).Returns(new Vector(1, 2, 3));
            Mobj.Setup(o => o.get_property("Position")).Returns(new Vector(1, 1, 1));
            StartMoveCommand smc = new StartMoveCommand(Mobj.Object);
            smc.Execute();
        }
    }
}


