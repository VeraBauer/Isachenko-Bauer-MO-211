using Hwdtech;
using Spaceship__Server;
using Moq;

namespace Spaceship.Collision.Tests;

public class UnitTest1
{
    public void SetupIoC()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Check.Collision.2D", (object [] args) => 
        {
            CollisionSolver solver= new();
            return (object)solver.Solve2D(args);
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Strategies.SolveTree", (object[] args) =>
        {
            TreeSolver solver = new();
            return  (object)solver.Solve(args);
        }).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapters.IUObject.IMovable", (object [] args) => 
        {
            Adapter adp= new();
            return  (object)adp.IUObjectToIMovable(args);
        }).Execute();
        
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Get.Solution.Tree", (object[] args) =>
        {
            Mock<ILeaf> l1 = new();
            Mock<ILeaf> l2 = new();
            Mock<ILeaf> l3 = new();
            Mock<ILeaf> l4 = new();
            Mock<ILeaf> l11 = new();
            Mock<ILeaf> l21 = new();
            Mock<ILeaf> l31 = new();
            Mock<ILeaf> l41 = new();
            l4.Setup(l => l.value).Returns(4);
            l3.Setup(l => l.value).Returns(3);
            l3.Setup(l => l.GetSons()).Returns(new SortedSet<ILeaf> {l4.Object});
            l2.Setup(l => l.value).Returns(2);
            l2.Setup(l => l.GetSons()).Returns(new SortedSet<ILeaf> {l3.Object});
            l1.Setup(l => l.value).Returns(1);
            l1.Setup(l => l.GetSons()).Returns(new SortedSet<ILeaf> {l2.Object});
            l41.Setup(l => l.value).Returns(5);
            l31.Setup(l => l.value).Returns(4);
            l31.Setup(l => l.GetSons()).Returns(new SortedSet<ILeaf> {l41.Object});
            l21.Setup(l => l.value).Returns(3);
            l21.Setup(l => l.GetSons()).Returns(new SortedSet<ILeaf> {l31.Object});
            l11.Setup(l => l.value).Returns(2);
            l11.Setup(l => l.GetSons()).Returns(new SortedSet<ILeaf> {l21.Object});
            return new SortedSet<ILeaf>{l11.Object, l1.Object};
        }).Execute();;
    }
    [Fact]
    public void Collision_True()
    {
        SetupIoC();
        Mock<IUObject> _obj1 = new();
        _obj1.Setup(o => o.get_property("Velocity")).Returns(new Vector(1, 1));
        _obj1.Setup(o => o.get_property("Position")).Returns(new Vector(1, 1));

        Mock<IUObject> _obj2 = new();
        _obj2.Setup(o => o.get_property("Velocity")).Returns(new Vector(4, 5));
        _obj2.Setup(o => o.get_property("Position")).Returns(new Vector(2, 3));

        Assert.True(Hwdtech.IoC.Resolve<bool>("Check.Collision.2D", _obj1.Object, _obj2.Object));
    }
}