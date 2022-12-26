
using Hwdtech;
using System.Collections.Generic;


namespace Spaceship__Server;

public class MacroCreator
{
    public IMacro CreateMacro(object[] args)
    {
        string call = (string) args[0]+".Get.Dependencies";
        List<string> dependencies = IoC.Resolve<List<string>>(call);
        IMacro result = IoC.Resolve<IMacro>((string)args[0], dependencies, (IUObject)args[1]);
        return result;
    }
}