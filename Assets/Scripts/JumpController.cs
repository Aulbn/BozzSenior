using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class JumpController : PlayerController
{

    public Transform[] positions;
    public float transitionTime = 1;
    public float jumpCooldown;
    public AnimationCurve jumpCurve, speedCurve;

    [Space]
    public MeshRenderer renderer;

    private Coroutine MoveCoroutine;
    private bool isMoving = false;

    protected override void OnSouth(InputValue value) {MoveToPosition(0);}
    protected override void OnWest(InputValue value) {MoveToPosition(1);}
    protected override void OnNorth(InputValue value) {MoveToPosition(3);}
    protected override void OnEast(InputValue value) {MoveToPosition(2);}

    private void Start()
    {
        renderer.material.SetColor("_BaseColor", Player.color);
    }

    public void MoveToPosition(int positionIndex)
    {
        if (!isMoving && transform.position != positions[positionIndex].position)
            MoveCoroutine = StartCoroutine(IEMoveToPosition(positionIndex));
    } 
    
    private IEnumerator IEMoveToPosition(int destinationIndex)
    {
        isMoving = true;

        float transition = 0;
        Vector3 startPos = transform.position;
        float value = transition / transitionTime;

        while (transition < transitionTime)
        {
            transform.position = Vector3.Lerp(startPos, positions[destinationIndex].position, speedCurve.Evaluate(value));
            transform.position = new Vector3(transform.position.x, transform.position.y * jumpCurve.Evaluate(speedCurve.Evaluate(value)), transform.position.z);

            yield return new WaitForEndOfFrame();
            transition += Time.deltaTime;
            value = transition / transitionTime;
        }
        transform.position = positions[destinationIndex].position;
        
        yield return new WaitForSecondsRealtime(jumpCooldown);
        isMoving = false;
    }
}
