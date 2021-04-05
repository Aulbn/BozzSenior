using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class CustomPlayerController : PlayerController
{
    [Serializable] public struct FaceButtonCollection
    {
        public UnityEvent onNorth;
        public UnityEvent onNorthUp;
        public UnityEvent onSouth;
        public UnityEvent onSouthUp;
        public UnityEvent onWest;
        public UnityEvent onWestUp;
        public UnityEvent onEast;
        public UnityEvent onEastUp;
    }
    //[Serializable] public struct DPadCollection
    //{
    //    public UnityEvent onUp;
    //    public UnityEvent onDown;
    //    public UnityEvent onLeft;
    //    public UnityEvent onRight;
    //}
    //public DPadCollection dpad;

    public UnityEvent onStart;
    public FaceButtonCollection faceButtons;
    public UnityEvent<Vector2> onMove;

    protected void Start()
    {
        SetHybridModel();
        onStart.Invoke();
    }
    protected override void OnNorth(InputAction.CallbackContext context) {faceButtons.onNorth.Invoke();}
    protected override void OnNorthUp(InputAction.CallbackContext context) {faceButtons.onNorthUp.Invoke();}
    protected override void OnSouth(InputAction.CallbackContext context) {faceButtons.onSouth.Invoke();}
    protected override void OnSouthUp(InputAction.CallbackContext context) {faceButtons.onSouthUp.Invoke();}
    protected override void OnEast(InputAction.CallbackContext context) {faceButtons.onEast.Invoke();}
    protected override void OnEastUp(InputAction.CallbackContext context) {faceButtons.onEastUp.Invoke();}
    protected override void OnWest(InputAction.CallbackContext context) {faceButtons.onWest.Invoke();}
    protected override void OnWestUp(InputAction.CallbackContext context) {faceButtons.onWestUp.Invoke();}
    protected override void OnMove(InputAction.CallbackContext context){onMove.Invoke(context.ReadValue<Vector2>());}

}
