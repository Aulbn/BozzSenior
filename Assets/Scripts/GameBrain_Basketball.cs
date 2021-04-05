using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrain_Basketball : MonoBehaviour
{
    [Header("Hoop")]
    public Transform hoop;
    public float hoopPathLength;
    public float hoopPathCenter;
    public float hoopSpeed;

    void Update()
    {
        hoop.position = new Vector3( hoopPathCenter + Mathf.PingPong(Time.time * hoopSpeed, hoopPathLength) - hoopPathLength / 2f, hoop.position.y, hoop.position.z);//move on x axis only
    }
}
