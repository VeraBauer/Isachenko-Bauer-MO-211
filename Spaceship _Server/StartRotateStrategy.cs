namespace Spaceship__Server;

public class StartRotateStrategy : IStrategy
{
	public object run_strategy(params object[] args)
	{
		IRotatable obj = (IRotatable)args[0];

		return new RotateCommand(obj);
	}
}

