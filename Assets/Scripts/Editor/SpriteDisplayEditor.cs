using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteDisplay))]
public class SpriteDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ((SpriteDisplay)target).SetSprite();
        serializedObject.ApplyModifiedProperties();
    }
}
