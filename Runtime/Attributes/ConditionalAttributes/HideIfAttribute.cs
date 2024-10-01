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

        public HideIfAttribute(string enumName, object enumValue) : base(enumName, enumValue as Enum) {
            ConditionType = EConditionType.SHOW;
            InvertResult = true;
        }
    }
}