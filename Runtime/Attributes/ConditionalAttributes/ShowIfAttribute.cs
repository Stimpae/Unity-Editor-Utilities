using System;

namespace TG.Attributes {
    public class ShowIfAttribute : ConditionAttributeBase {
        public ShowIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.SHOW;
        }

        public ShowIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.SHOW;
        }

        public ShowIfAttribute(string enumName, Enum enumValue) : base(enumName, enumValue) {
            ConditionType = EConditionType.SHOW;
        }
    }
}