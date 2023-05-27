using Hwdtech;
using System.Collections.Generic;
namespace Spaceship__Server;

public class GameInitCommand : Spaceship__Server.ICommand
{
    private int objamount;
    public GameInitCommand(int objects)
    {
        this.objamount = objects;
    }
    public void Execute()
    {
        Dictionary<string, object> GameObjects = new();

        for (int i = 0; i < this.objamount ; i++)
        {
            Dictionary<string, object> obj = new();

            IoC.Resolve<Spaceship__Server.ICommand>("IUObject.Property.Set", obj, "Position", Hwdtech.IoC.Resolve<Vector>("SetupPositionWallByWall", i)).Execute();
            IoC.Resolve<Spaceship__Server.ICommand>("IUObject.Property.Set", obj, "Fuel", 100.0).Execute();
            IoC.Resolve<Spaceship__Server.ICommand>("IUObject.Property.Set", obj, "OwnerID", ((i%2) + 1).ToString()).Execute();

            GameObjects.Add((i + 1).ToString(), (object)obj);
        }

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Current.ObjById", (object[] args) => 
        {
            return GameObjects[(string)args[0]];
        }).Execute();
    } 
}
