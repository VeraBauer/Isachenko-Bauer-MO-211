using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Runtime.CompilerServices;


namespace Spaceship__Server.Strategies
{
    public class MovableSetupable : IMovable
    {
        public Vector Speed { get; }
        public Vector Position { get; set; }
        public MovableSetupable(IUObject obj)
        {
            this.Speed = (Vector)obj.get_property("Speed");
            this.Position = (Vector)obj.get_property("Position");
        }
    }
    public class MoveCommandContiniousAdapter
    {
        public object Continious(object[] args)
        {
            IMovable mo = new MovableSetupable ((IUObject) args[0]);

            MoveCommand mc = new MoveCommand(mo);

            //IMacro command = null;
            Mock<IMacro> command = new();
            command.Setup(o => o.actions).Returns(new List<Spaceship__Server.ICommand>{mc, (ICommand)command});
            return (ICommand) command.Object;
        }
    }
}
