using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpriteManager : MonoBehaviour
{
    [Serializable]
    public struct ButtonFace
    {
        public Sprite pc, ps, xb;
    }

    public ButtonFace north, east, south, west;
}
