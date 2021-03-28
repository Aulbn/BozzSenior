using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPillar : MonoBehaviour
{
    //public Collider trigger;
    public float sinkSpeedMultiplier;
    public float riseSpeed;

    public bool isActive = true;

    private int nPlayers;
    private float originHeight;

    private void Start()
    {
        originHeight = transform.position.y;
    }

    void Update()
    {
        if (!isActive) return;

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
        if (other.CompareTag("Player") && other.transform.parent != transform)
        {
            nPlayers++;
            other.transform.SetParent(transform);
            Debug.Log("land: " + other.gameObject.name + ", " + nPlayers);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nPlayers--;
            other.transform.parent = null;
            Debug.Log("leave: " + nPlayers);
        }
    }

}
