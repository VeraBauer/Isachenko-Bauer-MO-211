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
            Moqueue.Setup(o => o.Enqueue(new object[5])).Verifiable();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IQueue.Push", (object[] args) =>
            {
                return Moqueue.Object.Enqueue(args);
            }).Execute();

            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IUObject.IMovable.Continious", (object[] args) =>
            {
                MoveCommandContinious mcc = new MoveCommandContinious();
                return mcc.Continious(args);
            }).Execute();
            

        }
    }
}


