using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioObject", menuName = "ScriptableObjects/Audio Object", order = 0)]
public class AudioObject : ScriptableObject
{
    public AudioClip AudioClip;
    [Range(0,1)]
    public float Volume = 1;
    //Sound category in sound mixer

    public void PlayOneShot()
    {
        //Spawn audio object player that will destroy (or pool) itself when done.
    }
}
