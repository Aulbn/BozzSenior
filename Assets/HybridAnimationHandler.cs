using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridAnimationHandler : MonoBehaviour
{
    public AnimationCollection AnimationCollection;
    public Animator Animator;



    public void PlayAnimation(string animationName)
    {
        Debug.Log("[PLAY ANIMATION] \"" + animationName +"\"");
        StopAllCoroutines();
        if (string.IsNullOrEmpty(AnimationCollection.GetAnimationStruct(animationName).AnimationName))
        {
            Debug.LogWarning("Tried getting an animation that doesn't exist.");
            return;
        }
        Animator.CrossFadeInFixedTime(animationName, .2f, 1);
        //Transition back to Null!

        //Animator.CrossFadeInFixedTime("Null", .2f, 1, 2f);
        //StartCoroutine(IEQueueStopAnimation(.2f, animationName));
    }
    public void StopAnimaiton()
    {
        Animator.CrossFadeInFixedTime("Null", .2f, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StopAnimaiton();
    }

    private IEnumerator IEQueueStopAnimation(float fadeTime, string animationToStop)
    {
        Debug.Log("Queue Stop Animation");
        //Debug.Log(Animator.GetCurrentAnimatorStateInfo(1).length);
        //Debug.Log("State is \"Null\": " + Animator.GetCurrentAnimatorStateInfo(1).IsName("Null") + " | Playername is: " + transform.parent.parent.GetComponent<PlayerController>().Player.playerName);
        //Debug.Log("State is \"Null\": " + Animator.GetNextAnimatorStateInfo(1).IsName("Null") + " | Playername is: " + transform.parent.parent.GetComponent<PlayerController>().Player.playerName);
        //while (Animator.GetAnimatorTransitionInfo(1).duration == 0) //Wait out the first transition
        //{
        //    yield return null;
        //}
        //while (Animator.GetAnimatorTransitionInfo(1).duration > 0)
        //{
        //    //var stateInfo = Animator.GetNextAnimatorStateInfo(1);
        //    ////Debug.Log("Normalized time (Next): " + stateInfo.normalizedTime % 1 + " | isHeadbutt: " + Animator.GetNextAnimatorStateInfo(1).IsName("Headbutt"));
        //    ////Debug.Log("Normalized time (Current): " + Animator.GetCurrentAnimatorStateInfo(1).normalizedTime % 1 + " | isNull: " + Animator.GetCurrentAnimatorStateInfo(1).IsName("Null"));
        //    ////if ((stateInfo.length / (stateInfo.normalizedTime % 1)) - stateInfo.length <= fadeTime)
        //    //if (stateInfo.length - (stateInfo.length * (stateInfo.normalizedTime % 1)) <= fadeTime)
        //    //{
        //    //    //Debug.Log("Stop animation! " + stateInfo.length + " | Norm:" + (stateInfo.normalizedTime % 1));
        //    //    //Debug.Log("Faded at " + (stateInfo.length - (stateInfo.length * (stateInfo.normalizedTime % 1))));
        //    //    //Animator.CrossFadeInFixedTime("Null", fadeTime, 1);
        //    //    Debug.Log("CrossFade to Null");
        //    //    break;
        //    //}
        //    Debug.Log(Animator.GetAnimatorTransitionInfo(1).normalizedTime);
        //    //Debug.Log("State is \"Null\": " + Animator.GetCurrentAnimatorStateInfo(1).IsName("Null"));
        //    Debug.Log("CurrentState is : " + (Animator.GetCurrentAnimatorStateInfo(1).IsName("Null") ? "Null" : "Headbutt (probably)") + " and next is: " + (Animator.GetNextAnimatorStateInfo(1).IsName("Headbutt") ? "Headbutt" : "Null (probably)"));
        //    //Debug.LogWarning("State is \"Null\": " + Animator.GetCurrentAnimatorStateInfo(1).IsName("Null") + " | Normalized Time: " + Animator.GetCurrentAnimatorStateInfo(1).normalizedTime);
        //    yield return null;
        //}

        while (true)
        {
            var stateInfo = Animator.GetCurrentAnimatorStateInfo(1).IsName(animationToStop) ? Animator.GetCurrentAnimatorStateInfo(1) : Animator.GetNextAnimatorStateInfo(1);
            //if (stateInfo.length - (stateInfo.length * (stateInfo.normalizedTime % 1)) <= fadeTime)
            //if (stateInfo.normalizedTime >= .8f)
            if (Animator.GetNextAnimatorClipInfoCount(1) == 0 && Animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.8f)
            {
                Animator.CrossFadeInFixedTime("Null", fadeTime, 1);
                Debug.Log("[AT STOP] CurrentState is : " + (Animator.GetCurrentAnimatorStateInfo(1).IsName("Headbutt") ? "Headbutt" : "Null (probably)") + " and next is: " + (Animator.GetNextAnimatorStateInfo(1).IsName("Headbutt") ? "Headbutt" : "Null (probably)"));
                break;
            }
            yield return null;
        }

    }

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
