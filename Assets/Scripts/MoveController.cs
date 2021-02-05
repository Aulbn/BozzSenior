using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : PlayerController
{
    public PlayerPosition[] positions;
    public bool canMove = false;

    public enum TravelType
    {
        Teleport, Walk, Run, Jump
    }

    private PlayerPosition currentPosition;

    protected override void OnNorth(InputValue value)
    {
        positions[3].MoveToPosition(this);
    }
    protected override void OnEast(InputValue value)
    {
        positions[2].MoveToPosition(this);

    }
    protected override void OnSouth(InputValue value)
    {
        positions[0].MoveToPosition(this);

    }
    protected override void OnWest(InputValue value)
    {
        positions[1].MoveToPosition(this);
    }

    public void MoveToPosition(Vector3 pos, TravelType travelType)
    {
        if (canMove)
            transform.position = pos;
    }

    public void SetPosition(PlayerPosition position)
    {
        if (currentPosition)
            currentPosition.RemoveController(this);
        currentPosition = position;
    }
}
