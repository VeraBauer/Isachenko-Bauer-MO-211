using Hwdtech;
using System.Linq;
using System.Collections.Generic;

namespace Spaceship__Server;

public class TreeDesicion
{
    public bool Decide(object[] args)
    {
        List<ILeaf> tree = (List<ILeaf>) args[0];
        int [] values = {(int)args[1],(int)args[2],(int)args[3],(int)args[4]};
        for(int i = 0; i < 4; i++)
        {
            List<int> currentValues = tree.Select(t => t.value).ToList();
            for(int j = 0; j < tree.Count; j++)
            {
                if(currentValues.Contains(values[i]))
                {
                    if (tree.Find(t => t.value == values[i]).GetSons() == null) return true;
                    tree = tree.Find(t => t.value == values[i]).GetSons();
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}