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
        DrawSpriteVariant("Playstation", ref sc.sv_playstation);
        DrawSpriteVariant("Xbox", ref sc.sv_xbox);
        DrawSpriteVariant("PC", ref sc.sv_pc);

    }

    private void DrawSpriteVariant(string variantName, ref SpriteCollection.SpriteVariant variant)
    {
        EditorGUILayout.LabelField(variantName, EditorStyles.centeredGreyMiniLabel);

        variant.sprite = (Sprite)EditorGUILayout.ObjectField("Icon", variant.sprite, typeof(Sprite), false);
        variant.backgroundSprite = (Sprite)EditorGUILayout.ObjectField("Background", variant.backgroundSprite, typeof(Sprite), false);

        variant.color = EditorGUILayout.ColorField(variant.color);
    }

}
