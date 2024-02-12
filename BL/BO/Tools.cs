using DalApi;
using System.Reflection;

namespace BO;

internal static class Tools
{
    public static string GenericToString(this object p)
    {
        var prop = p.GetType().GetProperties();
        string str = "";
        foreach (var property in prop)
        {
            if (property.Name == "Dependencies")
            {
                var dependenciesValue = property.GetValue(p);
                if (dependenciesValue != null)
                {
                    var dependenciesList = (System.Collections.IEnumerable)dependenciesValue;
                    str += $"{property.Name}: [";

                    foreach (var taskInList in dependenciesList)
                    {
                        var taskProperties = taskInList.GetType().GetProperties();
                        str += "{ ";

                        foreach (var taskProperty in taskProperties)
                        {
                            str += $"{taskProperty.Name}: {taskProperty.GetValue(taskInList)}, ";
                        }

                        str = str.TrimEnd(',', ' ') + " }, ";
                    }

                    str = str.TrimEnd(',', ' ') + "]";
                }
            }
            else
            {
                str += $" {property.Name}: {property.GetValue(p)},";
            }
        }

        return str.TrimEnd(',', ' ');
    }
    //public static string GenericToString(this object p)
    //{
    //    var props = p.GetType().GetProperties();
    //    string str = "";

    //    foreach (var prop in props)
    //    {
    //        if (prop.Name == "Dependencies")
    //        {
    //            str += $"{prop.Name}: [";
    //            var dependencies = (IEnumerable<TaskInList>?)prop.GetValue(p);

    //            if (dependencies != null)
    //            {
    //                foreach (var dependency in dependencies)
    //                {
    //                    str += dependency.ToString() + ", ";
    //                }
    //            }
    //            str = str.TrimEnd(',', ' ') + "]";
    //        }
    //        else
    //        {
    //            str += $" {prop.Name}: {prop.GetValue(p)},";
    //        }
    //    }

    //    return str.TrimEnd(',', ' ');
    //}



}



