using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor;


public class GameLoop : MonoBehaviour
{
    //List of parallel and sequencial items, including actions, waiting timers and conditional pausers.
    //Also count for how many times a loop will loop

    //Example:

    //Loop folder - put items inside, loops *set amount of times*.
    //Paralell event - plays *event* but doesn't stop flow.
    //Sequencial event - plays *event* and stops flow for a set amount of *seconds*.
    //Condition gate - compares a variable to a value, and pauses flow until it is true.

    //--OR--

    //Events (parallel)
    //Time gate
    //Condition gate

    [Serializable] public class GameLoopItem
    {
        public UnityEvent events;
        public float waitTime;
    }

    public bool playOnStart;
    [SerializeField] public List<GameLoopItem> gameEvents = new List<GameLoopItem>();

    private void Start()
    {
        if (playOnStart)
            StartLoop();
    }

    private IEnumerator IELoop()
    {
        foreach (GameLoopItem gameEvent in gameEvents)
        {
            gameEvent.events.Invoke();
            yield return new WaitForSeconds(gameEvent.waitTime);
        }
    }

    public void StartLoop()
    {
        StopLoop();
        StartCoroutine(IELoop());
    }

    public void StopLoop()
    {
        StopAllCoroutines();
    }

    public void DebugPrint(string debug)
    {
        Debug.Log("["+ gameObject.name +"] " + debug);
    }

}



