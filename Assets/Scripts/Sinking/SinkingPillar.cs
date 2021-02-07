using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPillar : MonoBehaviour
{
    //public Collider trigger;
    public float sinkSpeedMultiplier;
    public float riseSpeed;

    private int nPlayers;
    private float originHeight;

    private void Start()
    {
        originHeight = transform.position.y;
    }

    void Update()
    {
        if (nPlayers > 0)//Sink
        {
            transform.position -= Vector3.up * sinkSpeedMultiplier * nPlayers * Time.deltaTime;
        }
        else if(transform.position.y != originHeight)//Rise
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + riseSpeed * Time.deltaTime, transform.position.y, originHeight), transform.position.z);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nPlayers++;
            other.transform.SetParent(transform);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nPlayers--;
            other.transform.parent = null;
        }
    }

}
