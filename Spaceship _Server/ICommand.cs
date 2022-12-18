using System;
using System.Collections.Generic;

namespace Spaceship__Server
{
    public interface ICommand
    {
        public void Execute();
    }

    public interface IMacro : ICommand
    {
        IList<ICommand> actions { set; get; }
    }
}
