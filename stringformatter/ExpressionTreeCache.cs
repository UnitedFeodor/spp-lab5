using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace stringformatter
{
    public class ExpressionTreeCache
    {
        protected readonly ConcurrentDictionary<string, Func<object, string>> _cache = new();

        public string CacheString(string classMemberName, object target)
        {
            if (classMemberName.Contains('[') || classMemberName.Contains(']'))
            {
                throw new ArgumentException($"Invalid symbol '[' or ']' in classMemberName {classMemberName}");
            }

            string classMember = $"{target.GetType()}.{classMemberName}";

            if (_cache.ContainsKey(classMember))
            {
                return _cache.GetValueOrDefault(classMember)(target);
            }
            else
            {
                var fields = target.GetType().GetFields();
                var properties = target.GetType().GetProperties();
                Expression propertyOrField;
                if (fields.Where(field => field.Name == classMemberName).Any() ||
                    properties.Where(property => property.Name == classMemberName).Any())
                {
                    var objParam = Expression.Parameter(typeof(object), "obj");
                    propertyOrField = Expression.PropertyOrField(Expression.TypeAs(objParam, target.GetType()), classMemberName);
                    var toString = Expression.Call(propertyOrField, "ToString", null, null);
                    var func = Expression.Lambda<Func<object, string>>(toString, objParam).Compile();

                    return _cache.GetOrAdd(classMember, func)(target);
                }
                else
                {
                    throw new ArgumentException($"Target {target.GetType()} does not contain {classMemberName}");
                }
            }
        }

    }
}
