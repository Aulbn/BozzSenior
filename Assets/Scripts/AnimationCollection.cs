using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "AnimationCollection", menuName = "ScriptableObjects/AnimationCollection")]
public class AnimationCollection : ScriptableObject
{
    [Serializable]
    public struct AnimationStruct
    {
        public string AnimationName;
        public AnimationClip Animation;
    }

    public AnimatorController AnimatorController;
    public List<AnimationStruct> AbilityAnimations = new List<AnimationStruct>();

    public AnimationStruct GetAnimationStruct(string animationName)
    {
        foreach (AnimationStruct animation in AbilityAnimations)
        {
            if (animation.AnimationName.Equals(animationName))
                return animation;
        }
        return new AnimationStruct();
    }

}
