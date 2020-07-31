using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : PlayerController
{
    public Transform[] positions;
    public bool canWalk = false;

    protected override void OnNorth(InputValue value)
    {
        MoveToPosition(positions[0].position);
    }
    protected override void OnEast(InputValue value)
    {
        MoveToPosition(positions[3].position);

    }
    protected override void OnSouth(InputValue value)
    {
        MoveToPosition(positions[2].position);

    }
    protected override void OnWest(InputValue value)
    {
        MoveToPosition(positions[1].position);
    }

    private void MoveToPosition(Vector3 pos)
    {
        if (canWalk)
            transform.position = pos;
    }
}
