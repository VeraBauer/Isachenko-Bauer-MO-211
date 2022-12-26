using System.Collections.Generic;

namespace Spaceship__Server;

public class PushCommand : ICommand
{
    ICommand _cmd{get;}
    Queue<ICommand> _queue{get;}
    public PushCommand(ICommand cmd, Queue<ICommand> queue)
    {
        this._cmd = cmd;
        this._queue = queue;
    }

    public void Execute()
    {
        _queue.Enqueue(_cmd);
    }
}