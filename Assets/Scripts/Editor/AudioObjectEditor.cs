using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(AudioObject))]
public class AudioObjectEditor : Editor
{
    private VisualElement _Root;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AudioClip"));
        if (GUILayout.Button("P", GUILayout.Width(20)))
        {
            AudioClip clip = (AudioClip)serializedObject.FindProperty("AudioClip").objectReferenceValue;
            AudioUtility.PlayPreviewClip(clip);
            AudioUtility.IsPreviewClipPlaying(clip);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Volume"));

    }
}

//[CustomPropertyDrawer(typeof(AudioObject))]
//public class AudioObjectDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);
//        EditorGUI.LabelField(position, label);
//        EditorGUI.PropertyField(new Rect(position.position, new Vector2(position.width - 20, position.height)), property);
//        if (GUI.Button(new Rect(new Vector2(position.width, position.y), new Vector2(20, position.height)), "P"))
//        {
//            Debug.Log("Player preview of sound");
//        }
//        EditorGUI.EndProperty();
//    }
//}
