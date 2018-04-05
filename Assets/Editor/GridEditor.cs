using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GenerateShapes))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GenerateShapes myScript = (GenerateShapes)target;
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate();
        }
    }
}