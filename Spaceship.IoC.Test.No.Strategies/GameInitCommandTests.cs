using Hwdtech;
using Spaceship__Server;
namespace Spaceship.IoC.Test.No.Strategies;

public class GameInitCommandTests
{
    [Fact]
    public void RegisterDependency()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        var scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameInitCommand", (object[] args) => 
        {
            return new GameInitCommand((int)args[0]);
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetObjectPosition", (object[] args) => 
        {
            return ((Dictionary<string, object>)args[0])["Position"];
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetObjectFuel", (object[] args) => 
        {
            return ((Dictionary<string, object>)args[0])["Fuel"];
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetObjectOwnerId", (object[] args) => 
        {
            return ((Dictionary<string, object>)args[0])["OwnerID"];
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register" ,"SetupPositionWallByWall", (object[] args) => 
        {
            int i = (int) args[0];
            return new Vector((i - (i%2))*5, (i%2)*5);
        }).Execute();
    }
    [Fact]
    public void InitTest()
    {
        RegisterDependency();

        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("GameInitCommand", 6).Execute();

        Assert.Equal(new Vector(0, 0), Hwdtech.IoC.Resolve<Vector>("GetObjectPosition" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "1")));
        Assert.Equal(100, Hwdtech.IoC.Resolve<int>("GetObjectFuel" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "1")));
        Assert.Equal("1", Hwdtech.IoC.Resolve<string>("GetObjectOwnerId" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "1")));

        Assert.Equal(new Vector(0, 5), Hwdtech.IoC.Resolve<Vector>("GetObjectPosition" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "2")));
        Assert.Equal(100, Hwdtech.IoC.Resolve<int>("GetObjectFuel" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "2")));
        Assert.Equal("2", Hwdtech.IoC.Resolve<string>("GetObjectOwnerId" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "2")));

        Assert.Equal(new Vector(10, 0), Hwdtech.IoC.Resolve<Vector>("GetObjectPosition" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "3")));
        Assert.Equal(100, Hwdtech.IoC.Resolve<int>("GetObjectFuel" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "3")));
        Assert.Equal("1", Hwdtech.IoC.Resolve<string>("GetObjectOwnerId" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "3")));

        Assert.Equal(new Vector(10, 5), Hwdtech.IoC.Resolve<Vector>("GetObjectPosition" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "4")));
        Assert.Equal(100, Hwdtech.IoC.Resolve<int>("GetObjectFuel" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "4")));
        Assert.Equal("2", Hwdtech.IoC.Resolve<string>("GetObjectOwnerId" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "4")));

        Assert.Equal(new Vector(20, 0), Hwdtech.IoC.Resolve<Vector>("GetObjectPosition" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "5")));
        Assert.Equal(100, Hwdtech.IoC.Resolve<int>("GetObjectFuel" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "5")));
        Assert.Equal("1", Hwdtech.IoC.Resolve<string>("GetObjectOwnerId" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "5")));

        Assert.Equal(new Vector(20, 5), Hwdtech.IoC.Resolve<Vector>("GetObjectPosition" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "6")));
        Assert.Equal(100, Hwdtech.IoC.Resolve<int>("GetObjectFuel" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "6")));
        Assert.Equal("2", Hwdtech.IoC.Resolve<string>("GetObjectOwnerId" ,Hwdtech.IoC.Resolve<object>("Game.Current.ObjById", "6")));

    }
}
