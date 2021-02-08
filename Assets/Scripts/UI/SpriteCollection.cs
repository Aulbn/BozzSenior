using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SpriteCollection", menuName = "ScriptableObjects/SpriteCollection")]
public class SpriteCollection : ScriptableObject
{
    [Serializable]
    public struct SpriteVariant
    {
        public Sprite sprite, backgroundSprite;
        public Color color;
    }
    public SpriteVariant sv_playstation, sv_xbox, sv_pc;
}
