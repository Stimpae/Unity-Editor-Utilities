using System;

namespace TG.Attributes {
    public class EnabledIfAttribute : ConditionAttributeBase {
        public EnabledIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.ENABLED;
        }

        public EnabledIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.ENABLED;
        }

        public EnabledIfAttribute(string enumName, Enum enumValue) : base(enumName, enumValue) {
            ConditionType = EConditionType.ENABLED;
        }
    }
}