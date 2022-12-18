using Hwdtech;

namespace Spaceship__Server
{
    public interface IMoveCommandStartable : ICommand
    {
        IUObject _obj{ get; } 
    }

    public class StartMoveCommand : IMoveCommandStartable
    {
        public IUObject _obj {get;}
        public StartMoveCommand(IUObject obj)
        {
            this._obj = obj;
        }
        public void Execute()
        {
            ICommand mcContinious = IoC.Resolve<ICommand>("IUObject.Adapters.IMovable.Continious", this._obj);
            IoC.Resolve<ICommand>("IQueue.Push", mcContinious);
        }
    }
}