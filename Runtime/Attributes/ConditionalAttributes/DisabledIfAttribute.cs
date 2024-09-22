using System;

namespace TG.Attributes {
    public class DisabledIfAttribute : ConditionAttributeBase {
        public DisabledIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.ENABLED;
            InvertResult = true;
        }

        public DisabledIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.ENABLED;
            InvertResult = true;
        }

        public DisabledIfAttribute(string enumName, Enum enumValue) : base(enumName, enumValue) {
            ConditionType = EConditionType.ENABLED;
            InvertResult = true;
        }
    }
}