using Moq;

namespace Spaceship__Server
{
    public class Adapter
    {
        public IMovable IUObjectToIMovable(object[] args)
        {
            IUObject adaptable = (IUObject) args[0];

            Mock<IMovable> adapted =  new();

            adapted.Setup(a => a.Position).Returns((Vector)adaptable.get_property("Position"));
            adapted.Setup(a => a.Speed).Returns((Vector)adaptable.get_property("Velocity"));

            return adapted.Object;
        }
    }
}