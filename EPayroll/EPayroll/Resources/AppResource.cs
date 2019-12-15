using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Resources
{
    public class AppResource
    {
        private static readonly IDictionary<string, object> Dictionary = new Dictionary<string, object>();

        public static bool Add<TValue>(string key, TValue value)
        {
            try
            {
                Dictionary.Add(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Replace<TValue>(string key, TValue value)
        {
            try
            {
                if (Dictionary.ContainsKey(key))
                    Dictionary.Remove(key);
                Dictionary.Add(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Get<TValue>(string key, out TValue value)
        {
            Dictionary.TryGetValue(key, out object outValue);
            if (outValue is TValue)
            {
                value = (TValue)outValue;
                return true;
            }
            value = default;
            return false;
        }

        public static TValue Get<TValue>(string key)
        {
            Dictionary.TryGetValue(key, out object outValue);
            if (outValue is TValue) return (TValue)outValue;
            return default;
        }
    }
}
