using System.Reflection;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null).ToString()
            );

        }

    }
}
