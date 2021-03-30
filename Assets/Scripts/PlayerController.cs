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
    protected PlayerActions inputControls { get { return Player.InputControls; } }

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
            inputControls.Gameplay.Disable();
        else
            inputControls.Gameplay.Enable();
    }

    private void OnEnable()
    {
        if (!Player) return;

        inputControls.Gameplay.Move.performed += OnMove;

        inputControls.Gameplay.West.performed += OnWest;
        inputControls.Gameplay.West.canceled += OnWestUp;
        inputControls.Gameplay.East.performed += OnEast;
        inputControls.Gameplay.East.canceled += OnEastUp;
        inputControls.Gameplay.North.performed += OnNorth;
        inputControls.Gameplay.North.canceled += OnNorthUp;
        inputControls.Gameplay.South.performed += OnSouth;
        inputControls.Gameplay.South.canceled += OnSouthUp;

        //Wait one frame with enabling
        if (_delayedEnableCoroutine != null)
            StopCoroutine(_delayedEnableCoroutine);
        _delayedEnableCoroutine = StartCoroutine(IEDelayedEnable());
    }
    private void OnDisable()
    {
        if (!Player) return;
        inputControls.Gameplay.Move.performed -= OnMove;

        inputControls.Gameplay.West.performed -= OnWest;
        inputControls.Gameplay.West.canceled -= OnWestUp;
        inputControls.Gameplay.East.performed -= OnEast;
        inputControls.Gameplay.East.canceled -= OnEastUp;
        inputControls.Gameplay.North.performed -= OnNorth;
        inputControls.Gameplay.North.canceled -= OnNorthUp;
        inputControls.Gameplay.South.performed -= OnSouth;
        inputControls.Gameplay.South.canceled -= OnSouthUp;
    }

    private IEnumerator IEDelayedEnable()
    {
        yield return 0; //Wait one frame
        if (Player && inputLocks < 1)
        {
            inputControls.Gameplay.Enable();
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
