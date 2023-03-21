using System;

namespace Spaceship__Server;
public interface IThread
{

    void Stop();

    void HandleCommand();
    
    void UpdateBehaviour(Action newBehaviour);

    void Start();
}
