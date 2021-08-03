using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class CustomPlayerController : PlayerController
{
    //[Serializable] public struct FaceButtonCollection
    //{
    //    public UnityEvent onNorth;
    //    public UnityEvent onNorthUp;
    //    public UnityEvent onSouth;
    //    public UnityEvent onSouthUp;
    //    public UnityEvent onWest;
    //    public UnityEvent onWestUp;
    //    public UnityEvent onEast;
    //    public UnityEvent onEastUp;
    //}
    //[Serializable] public struct DPadCollection
    //{
    //    public UnityEvent onUp;
    //    public UnityEvent onDown;
    //    public UnityEvent onLeft;
    //    public UnityEvent onRight;
    //}
    //public DPadCollection dpad;

    [Serializable] public struct InputButton
    {
        public UnityEvent onDown;
        public UnityEvent onUp;
        //public UnityEvent onHold;
    }

    [Serializable] public struct InputButtons
    {
        public InputButton south, north, east, west;
    }

    public UnityEvent onStart;
    [Header("Input")]
    public InputButtons faceButtons;
    public UnityEvent<Vector2> onMove;

    protected void Start()
    {
        SpawnHybridModel();
        onStart.Invoke();
    }

    protected override void OnNorth(InputAction.CallbackContext context) { faceButtons.north.onDown.Invoke(); }
    protected override void OnNorthUp(InputAction.CallbackContext context) { faceButtons.north.onUp.Invoke(); }
    protected override void OnSouth(InputAction.CallbackContext context) { faceButtons.south.onDown.Invoke(); }
    protected override void OnSouthUp(InputAction.CallbackContext context) { faceButtons.south.onUp.Invoke(); }
    protected override void OnEast(InputAction.CallbackContext context) { faceButtons.east.onDown.Invoke(); }
    protected override void OnEastUp(InputAction.CallbackContext context) { faceButtons.east.onUp.Invoke(); }
    protected override void OnWest(InputAction.CallbackContext context) { faceButtons.west.onDown.Invoke(); }
    protected override void OnWestUp(InputAction.CallbackContext context) { faceButtons.west.onUp.Invoke(); }
    protected override void OnMove(InputAction.CallbackContext context){onMove.Invoke(context.ReadValue<Vector2>());}

}
