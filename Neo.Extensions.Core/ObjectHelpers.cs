using System;
using System.Linq;

namespace Neo.Extensions.Core
{
    public static class ObjectHelpers
    {
        public static bool CheckExistingProperty<T>(string property) where T : class
        {
            if (!string.IsNullOrEmpty(property))
                return typeof(T).GetProperties().Any(w => w.Name.ToLower() == property.ToLower());

            return false;
        }
    }
}
