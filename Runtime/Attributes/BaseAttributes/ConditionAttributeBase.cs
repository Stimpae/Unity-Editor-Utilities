using System;
using System.Diagnostics;

namespace TG.Attributes {
    public enum EConditionOperator {
        AND,
        OR,
    }
    
    public enum EConditionType{
        ENABLED,
        SHOW,
    }
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class ConditionAttributeBase : Attribute {
        public string[] Conditions { get; }
        public EConditionOperator ConditionOperator { get; }
        public EConditionType ConditionType { get; set; }
        public bool InvertResult { get; set; }
        public Enum EnumValue { get; set; }

        protected ConditionAttributeBase(string condition) : this(EConditionOperator.AND, condition) { }

        protected ConditionAttributeBase(EConditionOperator conditionOperator, params string[] conditions) {
            Conditions = conditions;
            ConditionOperator = conditionOperator;
        }

        protected ConditionAttributeBase(string enumName, Enum enumValue) : this(enumName) {
            EnumValue = enumValue ?? throw new ArgumentNullException(nameof(enumValue), "This parameter must be an enum value.");
        }
    }
}