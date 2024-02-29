namespace Spaceship__Server;

public class StartMoveStrategy : IStrategy
{
	public object run_strategy(params object[] args)
	{
		IMovable obj = (IMovable)args[0];
		
		return new MoveCommand(obj);				
	}
}
