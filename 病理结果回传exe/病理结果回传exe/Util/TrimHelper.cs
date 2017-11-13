
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SendPisResult.Util
{
    public static class TrimHelper
    {
        /// <summary>
        /// trim()实体的所有string属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void TrimAllPropertities<T>(T obj)
        {
            var type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[i];
                if (propertyInfo.PropertyType == typeof(string))
                {
                    string value = propertyInfo.GetValue(obj,null) as string;
                    if (value != null && propertyInfo.GetSetMethod(false)!=null)
                        propertyInfo.SetValue(obj, value.Trim(), null);
                }
            }
        }
    }
}
