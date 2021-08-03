using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SinkingPillar : MonoBehaviour
{
    public float sinkSpeedMultiplier;
    public float riseSpeed;

    public bool isActive = true;

    public VisualEffect BoilingVFX;

    private int nPlayers;
    private Vector3 originPos;

    private void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        if (!isActive) return;

        if (nPlayers > 0)//Sink
        {
            transform.position -= Vector3.up * sinkSpeedMultiplier * nPlayers * Time.deltaTime;
        }
        else if(transform.position != originPos)//Rise
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, riseSpeed * Time.deltaTime);
        }

        if ( BoilingVFX != null)
        {
            if (transform.position.y < 0.5f)
            {
                BoilingVFX.SetInt("Spawn Rate", 500);
            }
            else
            {
                BoilingVFX.SetInt("Spawn Rate", 0);
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.parent != transform)
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
