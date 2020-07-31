using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : PlayerController
{
    public Transform[] positions;

    protected override void OnNorth(InputValue value)
    {
        Debug.Log("North!");
    }
    protected override void OnEast(InputValue value)
    {

    }
    protected override void OnSouth(InputValue value)
    {

    }
    protected override void OnWest(InputValue value)
    {

    }

    private void MoveToPosition()
    {

    }
}
