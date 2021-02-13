using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteCollection))]
public class SpriteCollectionEditor : Editor
{
    SpriteCollection sc;

    private void OnEnable()
    {
        sc = (SpriteCollection)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        DrawSpriteVariant("Playstation", ref sc.sv_playstation);
        DrawSpriteVariant("Xbox", ref sc.sv_xbox);
        DrawSpriteVariant("PC", ref sc.sv_pc);

        if (EditorGUI.EndChangeCheck())
        {
            SaveProperties();
        }

        //if (GUILayout.Button("Save Properties")) //This could be one possibility, albeit a bit tedious if you forget to save
        //{
        //    SaveProperties();
        //}
    }

    private void SaveProperties() //This might give lag spikes, but it's only in editor
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        //Debug.Log("Hey");
    }

    private void DrawSpriteVariant(string variantName, ref SpriteCollection.SpriteVariant variant)
    {
        EditorGUILayout.LabelField(variantName, EditorStyles.centeredGreyMiniLabel);

        //serializedObject.FindProperty("sv_playstation"). = (Sprite)EditorGUILayout.ObjectField("Icon", variant.sprite, typeof(Sprite), false);
        variant.sprite = (Sprite)EditorGUILayout.ObjectField("Icon", variant.sprite, typeof(Sprite), false);
        //variant.backgroundSprite = (Sprite)EditorGUILayout.ObjectField("Background", variant.backgroundSprite, typeof(Sprite), false);
        variant.backgroundSprite = (Sprite)EditorGUILayout.ObjectField("Background", variant.backgroundSprite, typeof(Sprite), false);

        variant.color = EditorGUILayout.ColorField(variant.color);
    }

}
