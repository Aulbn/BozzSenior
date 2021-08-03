using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class JumpController : PlayerController
{

    public PlayerPosition[] positions;
    public float transitionTime = 1;
    public float JumpCooldown;
    public AnimationCurve jumpCurve, speedCurve;

    private Coroutine _MoveCoroutine;
    private bool _IsMoving = false;

    protected override void OnSouth(InputAction.CallbackContext context) {MoveToPosition(0);}
    protected override void OnWest(InputAction.CallbackContext context) {MoveToPosition(1);}
    protected override void OnNorth(InputAction.CallbackContext context) {MoveToPosition(3);}
    protected override void OnEast(InputAction.CallbackContext context) {MoveToPosition(2);}


    private void Start()
    {

    }

    public void MoveToPosition(int positionIndex)
    {
        if (!_IsMoving && !positions[positionIndex].Contains(this))
            _MoveCoroutine = StartCoroutine(IEMoveToPosition(positionIndex));
    } 
    
    private IEnumerator IEMoveToPosition(int destinationIndex)
    {
        _IsMoving = true;

        float transition = 0;
        Vector3 startPos = transform.position;
        float yValue;
        float lastYValue = 0;
        Vector3 direction = positions[destinationIndex].position.Flattened() - transform.position.Flattened();
        float rotationSpeed = 10f;

        HybridModel.Animator.SetTrigger("Jump");
        HybridModel.Animator.SetBool("IsAirborn", true);

        while (transition < transitionTime)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, Time.deltaTime * rotationSpeed, 0f), Vector3.up);
            transition += Time.deltaTime;
            float value = transition / transitionTime;
            yValue = jumpCurve.Evaluate(speedCurve.Evaluate(value));

            transform.position = Vector3.Lerp(startPos, positions[destinationIndex].GetMultiPosition(this), speedCurve.Evaluate(value));
            transform.position = new Vector3(transform.position.x, transform.position.y * yValue, transform.position.z);

            HybridModel.Animator.SetFloat("YVelocity", (yValue - lastYValue) * 60);
            lastYValue = yValue;

            yield return new WaitForEndOfFrame();
        }
        transform.position = positions[destinationIndex].GetMultiPosition(this);
        HybridModel.Animator.SetBool("IsAirborn", false);

        yield return new WaitForSecondsRealtime(JumpCooldown);
        _IsMoving = false;
    }

    public override void OnDeath()
    {
        PlayerPosition.ForceCancelReservation(this);
    }
}
