using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController_2 : PlayerController
{

    public Transform[] positions;
    public float transitionTime = 1;


    private Coroutine MoveCoroutine;
    private bool isMoving = false;

    protected override void OnSouth(InputValue value) {MoveToPosition(0);}
    protected override void OnWest(InputValue value) {MoveToPosition(1);}
    protected override void OnNorth(InputValue value) {MoveToPosition(2);}
    protected override void OnEast(InputValue value) {MoveToPosition(3);}

    public void MoveToPosition(int positionIndex)
    {
        if (!isMoving)
            MoveCoroutine = StartCoroutine(IEMoveToPosition(positions[positionIndex].position));
    }

    private IEnumerator IEMoveToPosition(Vector3 destination)
    {
        isMoving = true;

        float transition = 0;
        Vector3 startPos = transform.position;
        Vector3 mid = Vector3.Lerp(startPos, destination, 0.5f) - new Vector3(0, 1, 0);

        while (transition < transitionTime)
        {
            transform.position = Vector3.Slerp(startPos - mid, destination - mid, (transition / transitionTime)) + mid;

            yield return new WaitForEndOfFrame();
            transition += Time.deltaTime;
        }
        isMoving = false;
    }
}
