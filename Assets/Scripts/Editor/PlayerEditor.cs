using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Select Controller"))
        {
            UnityEditor.Selection.activeObject = ((Player)target).Controller.gameObject;
        }
    }
}
