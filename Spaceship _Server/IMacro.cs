using System.Collections.Generic;

namespace Spaceship__Server;

public interface IMacro : ICommand
{
    List<ICommand> jobs{get; set;}
}