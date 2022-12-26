using Hwdtech;
using System.Collections.Generic;


namespace Spaceship__Server;

public interface IMacro : ICommand
{
    public List<ICommand> _jobs{set; get;}
}
public class MacroCreator
{
    public IMacro CreateMacro(object[] args)
    {
        string call = (string) args[0]+".Get.Dependencies";
        List<string> dependencies = IoC.Resolve<List<string>>(call);
        ICommand result = IoC.Resolve<ICommand>((string)args[0], dependencies, (IUObject)args[1]);
    }
}