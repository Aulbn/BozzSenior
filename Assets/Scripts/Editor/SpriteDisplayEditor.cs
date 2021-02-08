using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SpriteDisplay))]
public class SpriteDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        ((SpriteDisplay)target).SetSprite();
        serializedObject.ApplyModifiedProperties();
    }
}
