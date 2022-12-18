using Hwdtech;
using Spaceship__Server;
using Spaceship__Server.Strategies;

namespace Spaceship.IoC.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

            Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("IUObject.Adapters.IMovable.Continious", (IUObject arg) => { new MoveCommandContinious.Continious(arg); });

        }
    }

}


