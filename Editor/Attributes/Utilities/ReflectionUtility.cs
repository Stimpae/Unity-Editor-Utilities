using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace TG.Attributes.Editor {
    public static class ReflectionUtility {
        public static object GetValue(object source, string name, int index = -1) {
            if (source == null) return null;
            var field = source.GetType().GetField(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            if (field == null) return null;
            var value = field.GetValue(source);
            return index < 0 ? value : (value as IEnumerable)?.Cast<object>().ElementAtOrDefault(index);
        }
        
        public static PropertyInfo GetProperty(object target, string propertyName) {
            return target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static FieldInfo GetField(object target, string fieldName) {
            return target.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        }
        public static FieldInfo[] GetFields(object target) {
            return target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).ToArray();
        }
        
        public static MethodInfo GetMethod(object target, string methodName) {
            return target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static MethodInfo[] GetMethods(object target, Func<MethodInfo, bool> predicate) {
            return target.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).Where(predicate).ToArray();
        }
    }
}