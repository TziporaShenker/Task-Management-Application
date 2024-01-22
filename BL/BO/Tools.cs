using DalApi;
using System.Reflection;

namespace BO;

internal static class Tools
{
    // פונקציה שמחזירה מחרוזת המייצגת את התכולת האובייקט
    public static string GenericToString(this object p)
    {
        var prop = p.GetType().GetProperties();
        string str = "";
        foreach (var property in prop)
        {
            str +=" "+ property.Name + ":" + property.GetValue(p);
        }
        return str;
    }
}
