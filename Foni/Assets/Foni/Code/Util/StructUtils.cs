using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Foni.Code.Util
{
    public static class StructUtils
    {
        public static string PrintStruct<T>(T obj) where T : struct
        {
            var objType = typeof(T);
            var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = objType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var propOutput = properties
                .ToList()
                .ConvertAll(property => $"{property.Name}: {property.GetValue(obj)}");

            var fieldOutput = fields
                .ToList()
                .ConvertAll(field => $"{field.Name}: {field.GetValue(obj)}");

            var combined = new List<string>();
            combined.AddRange(propOutput);
            combined.AddRange(fieldOutput);

            return $"{{ {string.Join(", ", combined)} }}";
        }
    }
}