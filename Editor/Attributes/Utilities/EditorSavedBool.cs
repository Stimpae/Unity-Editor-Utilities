using UnityEditor;

namespace EditorUtilities.Attributes.Editor {
    /// <summary>
    /// Save a bool value in the editor preferences
    /// </summary>
    public class EditorSavedBool {
        private readonly string m_name;
        public bool Value {
            get => EditorPrefs.GetBool(m_name);
            set => EditorPrefs.SetBool(m_name, value);
        }

        public EditorSavedBool(string name, bool defaultValue) {
            m_name = name;
            Value = EditorPrefs.GetBool(name, defaultValue);
        }
    }
}