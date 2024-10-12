using System;

namespace EditorUtilities.Attributes {
    public class DisabledIfAttribute : ConditionAttributeBase {
        public DisabledIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.ENABLED;
            InvertResult = true;
        }

        public DisabledIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.ENABLED;
            InvertResult = true;
        }

        public DisabledIfAttribute(string enumName, object enumValue) : base(enumName, enumValue as Enum) {
            ConditionType = EConditionType.ENABLED;
            InvertResult = true;
        }
    }
}