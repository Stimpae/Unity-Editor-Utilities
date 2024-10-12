using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectSetupTool : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    /*[MenuItem("Window/UI Toolkit/ProjectSetupTool")]
    public static void ShowExample()
    {
        ProjectSetupTool wnd = GetWindow<ProjectSetupTool>();
        wnd.titleContent = new GUIContent("ProjectSetupTool");
    }*/

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
