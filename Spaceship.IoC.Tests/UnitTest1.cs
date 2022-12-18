using Hwdtech;
using Spaceship__Server;
using Spaceship__Server.Strategies;
using System;

namespace Spaceship.IoC.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ContiniousMovementTest()
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();


            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IUObject.IMovable.Continious", (IUObject arg) =>
            {
                MoveCommandContinious mcc = new MoveCommandContinious();
                mcc.Continious(arg);
            }).Execute();
            
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IQueue.Push", (IUObject arg) =>
            {
                
            }).Execute();
        }
    }

}


