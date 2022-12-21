using Spaceship__Server;
using System.Collections.Generic;
using Hwdtech;

namespace Spaceship__Server
{
    public interface IMoveCommandStartable : Spaceship__Server.ICommand
    {
        IUObject _obj {set; get;}
        int Velocity{get; set;}
        Queue<Spaceship__Server.ICommand> _queue{get;set;}
    };

    public class StartMoveCommand : IMoveCommandStartable
    {
        public IUObject _obj{set; get;}
        public int Velocity{get; set;}
        public Queue<Spaceship__Server.ICommand> _queue{set;get;}
        public StartMoveCommand(IUObject order)
        {
            this._obj = (IUObject) order.get_property("Object");
            this.Velocity = (int) order.get_property("Velocity");
            this._queue = (Queue<Spaceship__Server.ICommand>)order.get_property("Queue");
        }
        public void Execute()
        {
            this._obj.set_property("Velocity", Velocity);

            ICommand mc = Hwdtech.IoC.Resolve<ICommand>("Adapters.IUObject.MoveCommand", _obj);

            _queue.Enqueue(mc);
        }
    };
}
