using Moq;

namespace Spaceship__Server
{
    public class Adapter
    {
         public class MovableSetupable : IMovable
    {
        public Vector Speed { get; }
        public Vector Position { get; set; }
        public MovableSetupable(IUObject obj)
        {
            this.Speed = (Vector)obj.get_property("Velocity");
            this.Position = (Vector)obj.get_property("Position");
        }
    }
        public IMovable IUObjectToIMovable(object[] args)
        {
            IUObject adaptable = (IUObject) args[0];

            IMovable adapted =(IMovable) new MovableSetupable(adaptable);

            return adapted;
        }
    }
}