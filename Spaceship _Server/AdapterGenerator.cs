using System;
using System.Reflection;

namespace Spaceship__Server;

public class AdapterGenerator
{
    public static string Generate(Type type)
    {
        PropertyInfo[] properties = type.GetProperties();
        string adapter = $"public class {type.Name}Adapter : {type.Name}\n";
        adapter += "{" + $"\n";
        foreach(PropertyInfo property in properties)
        {
            adapter += "\t public ";
            adapter += property.PropertyType.Name;
            adapter += " ";
            adapter += property.Name;
            adapter += " {";
            foreach(MethodInfo method in property.GetAccessors())
            {
                adapter += " " + method.Name.Substring(0, 3) + "; ";
            }
            adapter += "}" + $"\n";
        }
        adapter += "\t public " + $"{type.Name}Adapter" + " (object obj)" + $"\n" + "\t {" + $"\n";
        foreach(PropertyInfo property in properties)
        {
            adapter += "\t \t this." + property.Name + $" = Hwdtech.IoC.Resolve<{property.PropertyType.Name}>(\"IUObject.Property.Get\", obj, \"{property.Name}\");" + $"\n";
        }
        adapter += "\t }" + $"\n" + "}";

        return adapter;
    }
}
