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

    public void SetPlayer(Player player)
    {
        OnDisable();
        Player = player;
        OnEnable();
    }

    private void OnEnable()
    {
        if (inputLocks > 0) return;
        if (_delayedEnableCoroutine != null)
            StopCoroutine(_delayedEnableCoroutine);
        _delayedEnableCoroutine = StartCoroutine(IEDelayedEnable());
    }
    private void OnDisable()
    {
        if (!Player) return;
        Player.onMove -= OnMove;
        Player.onNorth -= OnNorth;
        Player.onNorthUp -= OnNorthUp;
        Player.onEast -= OnEast;
        Player.onEastUp -= OnEastUp;
        Player.onSouth -= OnSouth;
        Player.onSouthUp -= OnSouthUp;
        Player.onWest -= OnWest;
        Player.onWestUp -= OnWestUp;
    }

    private IEnumerator IEDelayedEnable()
    {
        yield return 0; //Wait one frame
        if (Player && inputLocks < 1)
        {
            Player.onMove += OnMove;
            Player.onNorth += OnNorth;
            Player.onNorthUp += OnNorthUp;
            Player.onEast += OnEast;
            Player.onEastUp += OnEastUp;
            Player.onSouth += OnSouth;
            Player.onSouthUp += OnSouthUp;
            Player.onWest += OnWest;
            Player.onWestUp += OnWestUp;
        }
    }

    protected virtual void OnMove(InputAction.CallbackContext context) { }
    protected virtual void OnNorth() { }
    protected virtual void OnNorthUp() { }
    protected virtual void OnEast() { }
    protected virtual void OnEastUp() { }
    protected virtual void OnSouth() { }
    protected virtual void OnSouthUp() { }
    protected virtual void OnWest() { }
    protected virtual void OnWestUp() { }

    public virtual void OnDeath() { }

    public void AddInputLock()
    {
        if (!Player) return;

        inputLocks++;
        if (inputLocks > 0)
            OnDisable();
    }
    public void RemoveInputLock()
    {
        if (!Player) return;

        inputLocks = Mathf.Clamp(--inputLocks, 0, int.MaxValue);
        if (inputLocks == 0)
            OnEnable();
    }
}
