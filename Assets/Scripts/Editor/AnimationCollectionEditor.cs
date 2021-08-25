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
            AnimatorController animatorController = collection.AnimatorController;
            var stateMachine = animatorController.layers[1].stateMachine;

            //Clear animations
            stateMachine.states = new ChildAnimatorState[0];

            var state = stateMachine.AddState("Null", new Vector3(250, 100));

            //Add animations
            AnimationCollection.AnimationStruct abilityAnimation;
            for (int i = 0; i < collection.AbilityAnimations.Count; i++)
            {
                abilityAnimation = collection.AbilityAnimations[i];
                state = stateMachine.AddState(abilityAnimation.AnimationName, new Vector3(30, 180 + 50 * i));
                state.motion = abilityAnimation.Animation;
            }
            EditorUtility.SetDirty(stateMachine);
            collection.AnimatorController.layers[1].stateMachine = stateMachine;
            EditorUtility.SetDirty(animatorController);
            AssetDatabase.SaveAssets();
        }
    }
}
