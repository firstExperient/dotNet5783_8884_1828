

namespace BlImplementation;

internal class Tools
{
    /// <summary>
    /// this function gets two object, and copy all the same propertys from one to the other
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    /// <param name="from">the object to copy from</param>
    /// <param name="to">the object to copy to</param>
    /// <returns>the modified object (in case it is by value)</returns>
    /// <exception cref="Exception">if one of the object is null, throw error</exception>
    public static S Copy<T, S>(T from, S to)
    {
        if (from == null || to == null)
            throw new BO.NullValueException("Must not specify null parameters");

        var fromProps = from.GetType().GetProperties();
        var toProps = to.GetType().GetProperties();
        foreach (var p in fromProps.Where(prop => prop.CanRead && prop.CanWrite))
        {
            var same = toProps.Where((prop) => prop.Name == p.Name && prop.PropertyType == p.PropertyType);
            if (same.Count() != 0)
            {
                object? value = p.GetValue(from);
                object boxed = to;
                same.First().SetValue(boxed, value);//will always contain only one, because there cannot be two props with the same name 
                to = (S)boxed;
            }
        }
        return to;
    }
}
