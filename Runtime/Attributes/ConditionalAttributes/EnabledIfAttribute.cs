using System;

namespace TG.Attributes {
    public class EnabledIfAttribute : ConditionAttributeBase {
        public EnabledIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.ENABLED;
        }

        public EnabledIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.ENABLED;
        }

        public EnabledIfAttribute(string enumName, object enumValue) : base(enumName, enumValue as Enum) {
            ConditionType = EConditionType.ENABLED;
        }
    }
}