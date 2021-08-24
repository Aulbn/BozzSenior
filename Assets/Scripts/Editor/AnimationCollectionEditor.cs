using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[CustomEditor(typeof(AnimationCollection))]
public class AnimationCollectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AnimationCollection collection = (AnimationCollection)target;
        if (GUILayout.Button("Update Animator"))
        {
            Debug.Log("Update Animator");
            UnityEditor.Animations.AnimatorController animatorController = collection.AnimatorController;
            var stateMachine = animatorController.layers[1].stateMachine;

            //Clear animations
            stateMachine.states = new ChildAnimatorState[0];

            stateMachine.AddState(new AnimatorState() { name = "Null" }, new Vector3(250, 100));

            //Add animations
            for (int i = 0; i < collection.AbilityAnimations.Count; i++)
            {
                var abilityAnimation = collection.AbilityAnimations[i];
                AnimatorState animatorState = new AnimatorState();
                animatorState.motion = abilityAnimation.Animation;
                animatorState.name = abilityAnimation.AnimationName;

                stateMachine.AddState(animatorState, new Vector3 (30, 180 + 50 * i));
            }


        }
    }
}
