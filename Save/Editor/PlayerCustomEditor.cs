using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SimpleCustomEditor))]
public class PlayerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SimpleCustomEditor generator = (SimpleCustomEditor)target;
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Next"))
        {
            generator.Customloafer();
        }
        if(GUILayout.Button("Color"))
        {
            generator.ColorChoice();
        }
        if(GUILayout.Button("Reset"))
        {
            generator.ResetNum();
        }
        EditorGUILayout.EndHorizontal();
    }
}
