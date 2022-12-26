using Hwdtech;
using Spaceship__Server;

namespace Spaceship.Macro.Test;

public class MacroBuilderTest
{
    [Fact]
    public void InitialTest()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ContiniousMovement.Get.Dependencies", (object[] args) =>
        {
            string[] deps = {"Velocity", "Position"};
            return deps;
        }).Execute();


        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "IoC.CreateMacro", (object[] args) =>
        {
            MacroCreator creator = new();
            return creator.CreateMacro(args);
        }).Execute();

        Console.WriteLine(Hwdtech.IoC.Resolve<Type>("IoC.Get.Dependencies","IMovable"));

    }
}