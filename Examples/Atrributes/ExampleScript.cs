using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace EditorUtilities.Attributes {
    [Serializable]
    public struct ExampleStruct {
        [ReadOnly] public string exampleString;
        public int exampleInt;
    }
    
    public class ExampleScript : MonoBehaviour {
        [ReadOnly] public int readOnlyInt = 141234;

        [Label("Example Label Of Fury", true)] public string exampleLabelString;
        [Label()] public string emptyLabelString;

        [InfoBox("Example information box, this is some information about this example script. we are using this to show off how things are done")]
        [Title("Example Title", "Example Subtitle", TitleAlignment.LEFT, true, true)]
        public float exampleFloat;
        public string exampleString;
        public bool exampleBool;
        
        [BoxGroup("Example Box Group", 2)] [SerializeField] private string exampleString4;
        [BoxGroup("Example Box Group")] [SerializeField] private bool exampleBool5;
        [BoxGroup("Example Box Group"), EnabledIf("exampleBool5")] [SerializeField] private float exampleEnableIf;
        [BoxGroup("Example Box Group"), ShowIf("exampleBool5")] [SerializeField] private int exampleShowIf;
        
        //[Struct] public ExampleStruct exampleStruct;
        [ScriptableObject] public ExampleScriptable exampleScriptable;
        
        [InlineButton("Inline Button", "ExampleMethod")] public int exampleInline;
        
        [Required("This field is required",true)] public GameObject requiredField; 
        [Validation("ValidateAnimator", "This field requires setup", true)] public Animator animator;
        public bool ValidateAnimator() { return ( animator == null); }

        [ListView] public List<int> exampleList = new List<int>();
        // min max slider
        [MinMax(5, 10)] public Vector2 minMaxVector;

        [FoldoutGroup("Example Foldout Group",5)] public int exampleInt;
        [FoldoutGroup("Example Foldout Group")] public float exampleFloat1;
        
        [Splitter(1, 20)]
        [Title("Example Title", "Example Subtitle", TitleAlignment.LEFT, true, true)]
        [Holder] public DecoratorHolder decoratorHolder;
        
        [ButtonField(nameof(ExampleMethod), "Example Button Field", 30)]
        [Holder] public DecoratorHolder decoratorHolder2;

        [InfoBox("Groups and buttons will always be displayed at the button of the inspector due to the way it is constructed in the editor.")]
        [Holder] public DecoratorHolder infoBox;
        
        [FoldoutGroup("Example Foldout Group 2", 4)] public string exampleString2;
        [FoldoutGroup("Example Foldout Group 2")] public bool exampleBool3;
        [FoldoutGroup("Example Foldout Group 2")] [MinMax(0, 10)] public Vector2 minMaxVector1;
        
        [Button("Example Button")]
        public void ExampleMethod() {
            Debug.Log("Example Method Called");
        }
    }
}
