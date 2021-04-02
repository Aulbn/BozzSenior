using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour
{
    public Player Player { get; private set; }
    public bool HasControl { get { return inputLocks == 0; } }
    private int inputLocks = 0;

    private Coroutine _delayedEnableCoroutine;
    protected PlayerActions InputControls { get { return Player.InputControls; } }

    public void SetPlayer(Player player)
    {
        OnDisable();
        Player = player;
        OnEnable();
    }

    public void AddInputLock()
    {
        if (!Player) return;

        inputLocks++;
        if (inputLocks > 0)
            ToggleInputLock(true);
    }
    public void RemoveInputLock()
    {
        if (!Player) return;

        inputLocks = Mathf.Clamp(--inputLocks, 0, int.MaxValue);
        if (inputLocks == 0)
            ToggleInputLock(false);
    }
    private void ToggleInputLock(bool isLocked)
    {
        if (isLocked)
            InputControls.Gameplay.Disable();
        else
            InputControls.Gameplay.Enable();
    }

    private void OnEnable()
    {
        if (!Player) return;

        InputControls.Gameplay.Move.performed += OnMove;

        InputControls.Gameplay.West.performed += OnWest;
        InputControls.Gameplay.West.canceled += OnWestUp;
        InputControls.Gameplay.East.performed += OnEast;
        InputControls.Gameplay.East.canceled += OnEastUp;
        InputControls.Gameplay.North.performed += OnNorth;
        InputControls.Gameplay.North.canceled += OnNorthUp;
        InputControls.Gameplay.South.performed += OnSouth;
        InputControls.Gameplay.South.canceled += OnSouthUp;

        //Wait one frame with enabling
        if (_delayedEnableCoroutine != null)
            StopCoroutine(_delayedEnableCoroutine);
        _delayedEnableCoroutine = StartCoroutine(IEDelayedEnable());
    }
    private void OnDisable()
    {
        if (!Player) return;
        InputControls.Gameplay.Move.performed -= OnMove;

        InputControls.Gameplay.West.performed -= OnWest;
        InputControls.Gameplay.West.canceled -= OnWestUp;
        InputControls.Gameplay.East.performed -= OnEast;
        InputControls.Gameplay.East.canceled -= OnEastUp;
        InputControls.Gameplay.North.performed -= OnNorth;
        InputControls.Gameplay.North.canceled -= OnNorthUp;
        InputControls.Gameplay.South.performed -= OnSouth;
        InputControls.Gameplay.South.canceled -= OnSouthUp;
    }

    private IEnumerator IEDelayedEnable()
    {
        yield return 0; //Wait one frame
        if (Player && inputLocks < 1)
        {
            InputControls.Gameplay.Enable();
            Debug.Log("Delayed Enable");
        }
    }

    protected virtual void OnMove(InputAction.CallbackContext context) { }
    protected virtual void OnNorth(InputAction.CallbackContext context) { }
    protected virtual void OnNorthUp(InputAction.CallbackContext context) { }
    protected virtual void OnEast(InputAction.CallbackContext context) { }
    protected virtual void OnEastUp(InputAction.CallbackContext context) { }
    protected virtual void OnSouth(InputAction.CallbackContext context) { }
    protected virtual void OnSouthUp(InputAction.CallbackContext context) { }
    protected virtual void OnWest(InputAction.CallbackContext context) { }
    protected virtual void OnWestUp(InputAction.CallbackContext context) { }

    public virtual void OnDeath() { }

}
