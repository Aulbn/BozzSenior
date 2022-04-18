using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Sound))]
public class SoundPropertyDrawer : PropertyDrawer
{
    private bool foldout;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(new Rect(position.position, new Vector2(position.width - 20, position.height)), property);

        foldout = EditorGUI.BeginFoldoutHeaderGroup(position, foldout, "Edit");
        if (foldout)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Volume"));
        }
        EditorGUI.EndFoldoutHeaderGroup();

        //EditorGUI.LabelField(position, label);
        //EditorGUI.PropertyField(new Rect(position.position, new Vector2(position.width - 20, position.height)), property);
        //if (GUI.Button(new Rect(new Vector2(position.width, position.y), new Vector2(20, position.height)), "P"))
        //{
        //    Debug.Log("Player preview of sound");
        //}
        EditorGUI.EndProperty();
    }
}
