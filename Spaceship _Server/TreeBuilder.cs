using System.Collections.Generic;
using System;

namespace Spaceship__Server;

public class TreeBuilder
{
    public Dictionary<int, object> tree = new();

    public TreeBuilder(object[] args)
    {
        List<string> lis = (List<string>)args[0];
        object Tree = this.tree;
        foreach (string str in lis)
        {
            List<Dictionary<int, object>> dict = new();
            string[] mas = str.Split(' ');
            List<int> list = new List<int>() { };
            foreach (var i in mas)
            {
                list.Add(Int32.Parse((string)i));
            }
            list.Reverse();
            object next = null;
            List<object> sos = new List<object>();
            foreach (int i in list)
            {
                if (next == null)
                {
                    next = i;
                    sos.Add(next);
                }
                else
                {
                    if (next is int)
                    {
                        int prev = (int)next;
                        next = new Dictionary<int, object>() { { i, prev } };
                        sos.Add(next);
                    }
                    else
                    {
                        Dictionary<int, object> prev = (Dictionary<int, object>)next;
                        next = new Dictionary<int, object>() { { i, prev } };
                        sos.Add(next);
                    }
                }
            }
            if (Tree == new List<Dictionary<int, object>>() { })
            {
                Tree = new List<Dictionary<int, object>>() { (Dictionary<int, object>)next };
            }
            else
            {
                list.Reverse();
                sos.Reverse();
                int counter = 0;

                object treee = new Dictionary<int, object>();

                while (!(treee is int))
                {
                    if (tree.TryGetValue(list[counter], out treee))
                    {
                        if (!(treee is List<int>))
                        {
                            tree = (Dictionary<int, object>)treee;
                        }
                        else
                        {
                            treee = (List<int>)treee;
                        }
                    }
                    else
                    {
                        tree.Add(counter, sos[counter + 1]);
                    }
                    counter++;
                }
            }
        }

    }
}
