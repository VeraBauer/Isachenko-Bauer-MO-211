using System;
using Spaceship__Server;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.IoC.Test.No.Strategies
{
    public class EndMoveCommandTest
    {
        [Fact]
        public void EndMoveCommand()
        {
            Queue<ICommand> _queue = new();

            Mock<ICommand> cmd = new();

            BridgeCommand bridge = new(cmd.Object);

            MacroCommand macro = new(_queue, new List<ICommand> { bridge });

            macro.Execute();

            Assert.Equal(2, _queue.Count);

            Assert.Equal(cmd.Object.GetType(), ((BridgeCommand)_queue.Peek()).internalCommand.GetType());

            _queue.Dequeue().Execute();

            _queue.Dequeue().Execute();

            bridge.Inject(new EmptyCommand());

            Assert.Equal("Spaceship__Server.EmptyCommand", ((BridgeCommand)_queue.Peek()).internalCommand.GetType().ToString());
        }
    }
}