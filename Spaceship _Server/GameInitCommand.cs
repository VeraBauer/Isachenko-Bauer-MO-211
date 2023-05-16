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
            obj.Add("Position", Hwdtech.IoC.Resolve<Vector>("SetupPositionWallByWall", i));
            obj.Add("Fuel", 100);
            obj.Add("OwnerID", ((i%2) + 1).ToString());
            GameObjects.Add((i + 1).ToString(), (object)obj);
        }

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Current.ObjById", (object[] args) => 
        {
            return GameObjects[(string)args[0]];
        }).Execute();
    } 
}