using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Test01))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI() 
    { 
        base.OnInspectorGUI();

        Test01 generator = (Test01)target;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Red Cubes",GUILayout.Width(150), GUILayout.Height(30))) 
        {
            generator.testInstace();
        }
        if (GUILayout.Button("Green Cubes", GUILayout.Width(150), GUILayout.Height(30)))
        {
            generator.testInstace();
        }
        if (GUILayout.Button("Blue Cubes", GUILayout.Width(150), GUILayout.Height(30)))
        {
            generator.testInstace();
        }
        EditorGUILayout.EndHorizontal();
        //GUISettings
        //GridBrushBase
        //GUIContent
        //GUIDrawer
        //AnimationCurve
        //EditorGUILayout.CurveField ("AnimationCurve Field", AnimationCurve.Linear (0, 3, 5, 5));
    }
}
