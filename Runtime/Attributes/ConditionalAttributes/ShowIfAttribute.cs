using System;

namespace TG.Attributes {
    public class ShowIfAttribute : ConditionAttributeBase {
        public ShowIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.SHOW;
        }

        public ShowIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.SHOW;
        }

        public ShowIfAttribute(string enumName, object enumValue) : base(enumName, enumValue as Enum) {
            ConditionType = EConditionType.SHOW;
        }
    }
}