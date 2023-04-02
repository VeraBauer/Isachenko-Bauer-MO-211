namespace Spaceship.IoC.Test.No.Strategies;

using Hwdtech;
using Spaceship__Server;
using Moq;

public class TreeBuilderTests
{
    [Fact]
    public void BuildTree()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Check.Collision", (object[] args) =>
        {
            CollisionSolver solver = new();
            return solver.Solve2D(args);
        }).Execute();

         IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "BuildTree", (object[] args) =>
        {
            return new TreeBuilder(args).tree;
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IUObject.IMovable", (object[] args) =>
        {
            MovableAdapter adp = new(args);
            return (object)adp;
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get.Solution.Tree", (object[] args) => {
            return Hwdtech.IoC.Resolve<Dictionary<int, object>>("BuildTree", new List<string>() { "1 1 1 1", "1 1 1 2", "1 1 2 2", "1 1 2 1", "1 1 2 4" });
        }).Execute();
 
        List<int> mas1 = new(){1, 2};
        List<int> mas2 = new(){2, 1, 4};
        Dictionary<int, object> dic = new();
        dic.Add(1, (object) mas1);
        dic.Add(2, (object) mas2);
        Dictionary<int, object> dic2 = new();
        dic2.Add(1, (object)dic);
        Dictionary<int, object> dic3 = new();
        dic3.Add(1, (object)dic2);
        Assert.Equal(dic3, Hwdtech.IoC.Resolve<Dictionary<int, object>>("Get.Solution.Tree"));

    }
}
