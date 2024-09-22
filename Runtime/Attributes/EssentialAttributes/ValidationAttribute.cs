using System;
using UnityEditor;

namespace TG.Attributes{
    public class ValidationAttribute : ValidateAttributeBase{
        public string ValidationCallback { get; private set; }
        public bool RegisterValidation { get; private set; }
        
        public ValidationAttribute(string validationCallback, string errorMessage = null, bool registerValidation = false) {
            ValidationCallback = validationCallback;
            ErrorMessage = errorMessage;
            RegisterValidation = registerValidation;
        }
    }
}