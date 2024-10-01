using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace TG.Attributes.Editor {
    public static class PropertyUtility {
        public static List<SerializedProperty> GetSerializedProperties(SerializedObject target) {
            var properties = new List<SerializedProperty>();
            var iterator = target.GetIterator();
            if (!iterator.NextVisible(true)) return properties;
            do {
                properties.Add(target.FindProperty(iterator.name));
            } while (iterator.NextVisible(false));
            return properties;
        }
        
        public static System.Type GetPropertyType(SerializedProperty property) {
            var type = property.serializedObject.targetObject.GetType();
            var field = type.GetField(property.propertyPath, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            return field.FieldType;
        }
    }
}