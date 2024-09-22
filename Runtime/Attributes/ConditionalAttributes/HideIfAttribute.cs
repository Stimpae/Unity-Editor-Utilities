using System;

namespace TG.Attributes {
    public class HideIfAttribute : ConditionAttributeBase {
        public HideIfAttribute(string condition) : base(condition) {
            ConditionType = EConditionType.SHOW;
            InvertResult = true;
        }

        public HideIfAttribute(EConditionOperator conditionOperator, params string[] conditions) : base(conditionOperator, conditions) {
            ConditionType = EConditionType.SHOW;
            InvertResult = true;
        }

        public HideIfAttribute(string enumName, Enum enumValue) : base(enumName, enumValue) {
            ConditionType = EConditionType.SHOW;
            InvertResult = true;
        }
    }
}