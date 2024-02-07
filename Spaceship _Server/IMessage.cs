using System.Collections.Generic;

namespace Spaceship__Server;

public interface IMessage
{
	public string cmd {get;}
	public string gameId {get;}
	public string objId {get;}
	public IDictionary<string, object> properties {get;}
}

