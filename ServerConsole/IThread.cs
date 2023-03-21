namespace Spaceship__Server;
public interface IThread
{
    public Thread thread;
    public IReciver receiver;
    public bool stop = false;
    public Action strategy;

    internal void Stop();

    internal void HandleCommand();
    
    internal void UpdateBehaviour(Action newBehaviour);
    public void Start();
    
}