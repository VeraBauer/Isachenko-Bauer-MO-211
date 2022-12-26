using Moq;
using System.Collections.Generic;
using Hwdtech;


namespace Spaceship__Server;


public class IoCStartMoveCommand
{
    public StartMoveCommand CreateStartMoveCommand(object[] args)
    {
        List<string> deps = (List<string>) args[0];
        IUObject obj = (IUObject) args[1];
        Mock<IUObject> order = new();

        for (int i = 0; i < deps.Count; i++)
        {
            order.Setup(o => o.get_property(deps[i])).Returns(obj.get_property(deps[i]));
        }

        return (new StartMoveCommand(order.Object));
    }
}