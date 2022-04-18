using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Sound 
{
    public AudioObject AudioObject;
    [Range(0,1)]
    public float Volume = 1;
    [Range(0, 1)]
    public float SpatialBlend = 0;
    [Range(-3,3)]
    public float Pitch = 1;

    public Sound(AudioObject audioOject, float volume, float pitch = 0)
    {
        AudioObject = audioOject;
        Volume = volume;
        Pitch = pitch;
    }
}
