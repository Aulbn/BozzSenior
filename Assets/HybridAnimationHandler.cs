using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridAnimationHandler : MonoBehaviour
{
    public AnimationCollection AnimationCollection;
    public Animator Animator;

    public void PlayAnimation(string animationName)
    {
        StopAllCoroutines();
        if (string.IsNullOrEmpty(AnimationCollection.GetAnimationStruct(animationName).AnimationName))
        {
            Debug.Log("Tried getting an animation that doesn't exist.");
            return;
        }
        Animator.CrossFadeInFixedTime(animationName, .2f, 1);
        //Transition back to Null!

        //Animator.CrossFadeInFixedTime("Null", .2f, 1, 2f);
        //StartCoroutine(IEQueueStopAnimation(.1f));
    }
    public void StopAnimaiton()
    {
        Animator.CrossFadeInFixedTime("Null", .2f, 1);
    }

    //private IEnumerator IEQueueStopAnimation(float fadeTime)
    //{
    //    Debug.Log(Animator.GetCurrentAnimatorStateInfo(1).length);
    //    while (true)
    //    {
    //        var stateInfo = Animator.GetCurrentAnimatorStateInfo(1);
    //        if ((stateInfo.length / stateInfo.normalizedTime) - stateInfo.length <= fadeTime)
    //        {
    //            Animator.CrossFadeInFixedTime("Null", fadeTime, 1);
    //            break;
    //        }
    //        yield return null;
    //    } 
    //}

    public void SetFloat(string name, float value)
    {
        Animator.SetFloat(name, value);
    }
    public void SetBool(string name, bool value)
    {
        Animator.SetBool(name, value);
    }
    public void SetTrigger(string name)
    {
        Animator.SetTrigger(name);
    }

}
