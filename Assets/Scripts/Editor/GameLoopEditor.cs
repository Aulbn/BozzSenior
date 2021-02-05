using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//[CustomEditor(typeof(GameLoop))]
public class GameLoopEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
		list = new ReorderableList(serializedObject,
				serializedObject.FindProperty("gameEvents"),
				true, true, true, true);
		
		list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

            var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.height = 20;
            //EditorGUILayout.PropertyField(element);
            //EditorGUI.PropertyField(rect, element.FindPropertyRelative("events"));
            //EditorGUILayout.PropertyField(element.FindPropertyRelative("events"));
            //EditorGUILayout.PropertyField(element, true);
            rect.y += 2;
            //rect.height = 10;
            //EditorGUI.PropertyField(rect, element);
            //EditorGUI.PropertyField(
            //	new Rect(rect.x, rect.y, rect.width, 1000),
            //	element.FindPropertyRelative("events"), GUIContent.none);

            SerializedProperty prop = element.FindPropertyRelative("events");
            //list.elementHeight = 200;

            //element.rectValue = new Rect(rect.x, rect.y, rect.width, 200);

            //EditorGUI.PropertyField(
            //	new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, rect.height),
            //	prop, true);
            //if (isActive)
            //{
            //EditorGUI.PropertyField(
            //    new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
            //    element.FindPropertyRelative("waitTime"), GUIContent.none);

            bool foldout = true; //declare outside of function
            //showGeneralSettings = EditorGUILayout.Foldout(showGeneralSettings, "hEJ");
            foldout = EditorGUI.Foldout(rect, false, "yo");

            //EditorGUI.PropertyField(
            //    new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, rect.height),
            //    element.FindPropertyRelative("events"), true);
            //}

            //list.elementHeightCallback = 
        };

        //list.elementHeightCallback = (index) =>
        //{
        //    return 
        //};
	}

    public override void OnInspectorGUI()
	{
		serializedObject.Update();
		//EditorGUILayout.PropertyField(serializedObject.FindProperty("gameEvents"));
		//SerializedProperty list = serializedObject.FindProperty("gameEvents");
		//EditorGUILayout.PropertyField(list);
		//EditorGUI.indentLevel += 1;
		//for (int i = 0; i < list.arraySize; i++)
		//{
		//	EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
		//}
		//EditorGUI.indentLevel -= 1;
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}
