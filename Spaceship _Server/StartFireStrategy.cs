namespace Spaceship__Server;

using Hwdtech;
using System.Collections.Generic;

public class StartFireStrategy: IStrategy
{
	public object run_strategy(params object[] args)
	{
		Dictionary<string, object> target = (Dictionary<string, object>)args[0];

		return IoC.Resolve<Spaceship__Server.ICommand>("Object.Health.GetDamaged", target);
	}
}
