using System;
using CoreWCF;
using System.Collections.Generic;
using System.Collections;


namespace Spaceship__Server
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SpaceshipApi : ISpaceshipApi
    {
        public JSONContract Message(JSONContract req)
        {   
            Dependencies.Run();
            
            Dictionary<string, object> content = (Dictionary<string, object>)req.Value.Entries;

            Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Deserialize Message to Command", content).Execute();

           return req;
        }
    }
}
