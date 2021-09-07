using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SinkingPillar : MonoBehaviour
{
    public float sinkSpeedMultiplier;
    public float riseSpeed;

    public bool isActive = true;

    [Header("References")]
    public VisualEffect BoilingVFX;
    public BoxCollider Collider;

    private int nPlayers;
    private Vector3 originPos;

    private void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        if (!isActive) return;

        //Count players within inside box
        nPlayers = 0;
        foreach (PlayerController c in GameManager.Controllers)
        {
            if (Collider.bounds.Contains(c.transform.position))
            {
                nPlayers++;
                c.transform.transform.SetParent(transform);
            }
            else if (c.transform.parent == transform)
                c.transform.transform.SetParent(null);
        }

        if (nPlayers > 0)//Sink
        {
            transform.position -= Vector3.up * sinkSpeedMultiplier * nPlayers * Time.deltaTime;
        }
        else if(transform.position != originPos)//Rise
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, riseSpeed * Time.deltaTime);
        }

        //Enable / disable boiling vfx
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
}
