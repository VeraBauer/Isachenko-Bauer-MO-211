using System;
using System.Collections.Generic;


namespace Spaceship__Server
{
    public interface IMovable
    {
        List<int> Speed { get;}
        List<int> Position { get; set; }
    }

    public interface ICommand
    {
        public void Execute();
    }

    public class MoveCommand : ICommand
    {
        IMovable _obj;
        public MoveCommand(IMovable obj)
        {
            _obj = obj;
        }

        public void Execute()
        {
            if (_obj.Speed.Count != _obj.Position.Count)
            {
                throw new Exception();
            }
            List<int> newpos = _obj.Position;
            for (int i = 0; i < _obj.Position.Count; i++)
            {
                newpos[i] += _obj.Speed[i];
            }
            _obj.Position = newpos;
        }

    }
}
