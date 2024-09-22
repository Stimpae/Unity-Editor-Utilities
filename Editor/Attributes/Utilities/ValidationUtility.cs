using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace TG.Attributes.Editor {
    public static class ValidationUtility {
        private static Dictionary<string, SerializedProperty> _failedValidations = new Dictionary<string, SerializedProperty>();

        public static void AddFailedValidation(SerializedProperty property) {
            _failedValidations.TryAdd(property.name + "-" + property.serializedObject.targetObject.GetInstanceID(), property);
        }

        public static void RemoveFailedValidation(SerializedProperty property) {
            _failedValidations.Remove(property.name + "-" + property.serializedObject.targetObject.GetInstanceID());
        }

        public static void ClearFailedValidations() {
            _failedValidations.Clear();
        }

        public static Dictionary<string, SerializedProperty> GetFailedValidations() {
            return _failedValidations;
        }

        public static SerializedProperty GetFailedValidation(string key) {
            _failedValidations.TryGetValue(key, out var property);
            return property;
        }

        public static void ValidateProperty(SerializedProperty property) {
            var attribute = AttributeUtility.GetAttribute<ValidateAttributeBase>(property);
            if (attribute is RequiredAttribute) {
                RequiredPropertyValidator(property);
            } else if (attribute is ValidationAttribute) {
                ValidationPropertyValidator(property);
            }
        }

        private static void RequiredPropertyValidator(SerializedProperty property) {
            var requiredAttribute = AttributeUtility.GetAttribute<RequiredAttribute>(property);
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null) {
                var errorMessage = string.IsNullOrEmpty(requiredAttribute.ErrorMessage) ? property.name + " is required" : requiredAttribute.ErrorMessage;
                AttributesGUI.DrawHelpBox(errorMessage, MessageType.Error, AttributeEditorStyles.GetColor(11), Color.white);
                if (requiredAttribute.RegisterValidation) AddFailedValidation(property);
            } else {
                RemoveFailedValidation(property);
            }
        }

        private static void ValidationPropertyValidator(SerializedProperty property) {
            var validateInputAttribute = AttributeUtility.GetAttribute<ValidationAttribute>(property);
            var target = AttributeUtility.GetTargetObject(property);
            var validationCallback = ReflectionUtility.GetMethod(target, validateInputAttribute.ValidationCallback);

            if (validationCallback != null && validationCallback.ReturnType == typeof(bool)) {
                var callbackParameters = validationCallback.GetParameters();
                var fieldInfo = ReflectionUtility.GetField(target, property.name);
                var fieldType = fieldInfo.FieldType;

                if (callbackParameters.Length == 0 || (callbackParameters.Length == 1 && fieldType == callbackParameters[0].ParameterType)) {
                    var result = (bool)validationCallback.Invoke(target, callbackParameters.Length == 0 ? null : new object[] { fieldInfo.GetValue(target) });
                    if (result) {
                        AttributesGUI.DrawHelpBox(validateInputAttribute.ErrorMessage, MessageType.Error, AttributeEditorStyles.GetColor(11), Color.white);
                        if (validateInputAttribute.RegisterValidation) AddFailedValidation(property);
                    } else {
                        RemoveFailedValidation(property);
                    }
                }
            }
        }
    }
}