using DalApi;
using System.Reflection;

namespace BO;

internal static class Tools
{
    // פונקציה שמחזירה מחרוזת המייצגת את התכולת האובייקט
    public static string ToString/*Property*/<T>(this T obj)
    {
        // קבלת מאפיינים של אובייקט מסוג T
        PropertyInfo[] properties = typeof(T).GetProperties();

        // בניית מחרוזת המייצגת את האובייקט
        string result = string.Join(", ", properties.Select(property =>
        {
            // קבלת הערך של המאפיין הנוכחי
            object? value = property.GetValue(obj);
            string valueString;

            // בדיקה האם הערך הוא null
            if (value == null)
            {
                valueString = "null";
            }
            // בדיקה האם הערך הוא קבוצה (IEnumerable)
            else if (value is IEnumerable<object> enumerableValue)
            {
                // אם זה כך, המרת כל איבר בקבוצה למחרוזת ושורפת יחודיות
                valueString = string.Join(", ", enumerableValue.Select(item => item.ToString()));
            }
            // אם אין קבוצה או null, המרת הערך למחרוזת
            else
            {
                valueString = value.ToString();
            }
            // בניית המחרוזת שמייצגת את המאפיין והערך שלו
            return $"{property.Name}: {valueString}";
        }));
        // החזרת התוצאה
        return result;
    }
}
