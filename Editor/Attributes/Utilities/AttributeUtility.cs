using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorUtilities.Attributes.Editor {
    public static class AttributeUtility {
        public static T[] GetAttributes<T>(SerializedProperty property) where T : class {
            var fieldInfo = ReflectionUtility.GetField(GetTargetObject(property), property.name);
            return fieldInfo == null ? new T[] { } : (T[])fieldInfo.GetCustomAttributes(typeof(T), true);
        }

        public static T GetAttribute<T>(SerializedProperty property) where T : class {
            return GetAttributes<T>(property)?.FirstOrDefault();
        }

        public static object GetTargetObject(SerializedProperty property) {
            if (property == null) {Debug.LogError("Property is null"); return null;}
            var path = property?.propertyPath.Replace(".Array.data[", "[");
            return path?.Split('.').SkipLast(1).Aggregate<string, object>(property.serializedObject.targetObject,
                (current, element) =>
                    element.Contains("[")
                        ? ReflectionUtility.GetValue(current, element[..element.IndexOf("[", StringComparison.Ordinal)],
                            int.Parse(element.Split('[', ']')[1]))
                        : ReflectionUtility.GetValue(current, element));
        }

        public static GUIContent GetLabel(SerializedProperty property) {
            var attribute = GetAttribute<LabelAttribute>(property);
            var labelText = (attribute != null) ? attribute.label : property.displayName;
            var labelContent = new GUIContent(labelText);
            if (labelText == string.Empty) labelContent = new GUIContent(GUIContent.none);
            return labelContent;
        }
        
        public static bool IsVisible(SerializedProperty property) {
            var conditionalAttribute = GetAttribute<ConditionAttributeBase>(property);
            if (conditionalAttribute is not { ConditionType: EConditionType.SHOW }) return true;
            return HandleConditionalAttributes(property, EConditionType.SHOW);
        }
        
        public static bool IsEnabled(SerializedProperty property) {
            var readOnlyAttribute = GetAttribute<ReadOnlyAttribute>(property);
            if (readOnlyAttribute != null) return false;
            return HandleConditionalAttributes(property, EConditionType.ENABLED);
        }

        private static bool HandleConditionalAttributes(SerializedProperty property, EConditionType conditionType) {
            var conditionalAttribute = GetAttribute<ConditionAttributeBase>(property);
            if (conditionalAttribute == null) return true;
            if (conditionalAttribute.ConditionType != conditionType) return true;
            
            var targetObject = GetTargetObject(property);
            if(conditionalAttribute.EnumValue != null) {
                var enumValue = GetEnumValue(targetObject, conditionalAttribute.Conditions[0]);
                if (enumValue == null) {
                    Debug.LogError($"Enum value {conditionalAttribute.Conditions[0]} not found in {targetObject.GetType().Name}");
                    return false;
                }
                
                var enumMatch = enumValue.Equals(conditionalAttribute.EnumValue);
                return enumMatch != conditionalAttribute.InvertResult;
            }
            
            var conditionValues = GetConditionValues(targetObject, conditionalAttribute.Conditions);
            if(conditionValues.Count == 0) {
                Debug.LogError($"Condition values not found in {targetObject.GetType().Name}");
                return false;
            }
            
            var conditionsFlag = GetConditionsFlag(conditionValues, conditionalAttribute.ConditionOperator, conditionalAttribute.InvertResult);
            return conditionsFlag;
        }

       

        private static Enum GetEnumValue(object target, string enumName) {
            var members = new MemberInfo[] {
                ReflectionUtility.GetField(target, enumName), ReflectionUtility.GetProperty(target, enumName),
                ReflectionUtility.GetMethod(target, enumName)
            };

            foreach (var member in members) {
                if (member is FieldInfo field && field.FieldType.IsSubclassOf(typeof(Enum)))
                    return (Enum)field.GetValue(target);

                if (member is PropertyInfo property && property.PropertyType.IsSubclassOf(typeof(Enum)))
                    return (Enum)property.GetValue(target);

                if (member is MethodInfo method && method.ReturnType.IsSubclassOf(typeof(Enum)))
                    return (Enum)method.Invoke(target, null);
            }

            return null;
        }

        private static List<bool> GetConditionValues(object target, string[] conditions) {
            var conditionValues = new List<bool>();
            foreach (var condition in conditions) {
                var members = new MemberInfo[] {
                    ReflectionUtility.GetField(target, condition), ReflectionUtility.GetProperty(target, condition),
                    ReflectionUtility.GetMethod(target, condition)
                };

                foreach (var member in members) {
                    switch (member) {
                        case FieldInfo field when field.FieldType == typeof(bool):
                            conditionValues.Add((bool)field.GetValue(target));
                            break;
                        case PropertyInfo property when property.PropertyType == typeof(bool):
                            conditionValues.Add((bool)property.GetValue(target));
                            break;
                        case MethodInfo method when method.ReturnType == typeof(bool) && method.GetParameters().Length == 0:
                            conditionValues.Add((bool)method.Invoke(target, null));
                            break;
                    }
                }
            }
            return conditionValues;
        }
        
        private static bool GetConditionsFlag(List<bool> conditionValues, EConditionOperator conditionOperator, bool invert) {
            var flag = conditionOperator == EConditionOperator.AND ? conditionValues.All(value => value) : conditionValues.Any(value => value);
            return invert ? !flag : flag;
        }
    }
}