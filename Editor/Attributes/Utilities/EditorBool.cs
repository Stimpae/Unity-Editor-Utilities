using UnityEditor;

namespace TG.Attributes.Editor {
    /// <summary>
    /// Save a bool value in the editor preferences
    /// </summary>
    public class EditorBool {
        private readonly string m_name;
        public bool Value {
            get => EditorPrefs.GetBool(m_name);
            set => EditorPrefs.SetBool(m_name, value);
        }

        public EditorBool(string name, bool defaultValue) {
            m_name = name;
            Value = EditorPrefs.GetBool(name, defaultValue);
        }
    }
}