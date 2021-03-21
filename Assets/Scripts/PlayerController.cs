using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour
{
    public Player Player { get; private set; }
    public bool HasControl { get { return inputLocks == 0; } }
    private int inputLocks;

    private Coroutine _delayedEnableCoroutine;

    public void SetPlayer(Player player)
    {
        OnDisable();
        Player = player;
        OnEnable();
    }

    private void OnEnable()
    {
        if (_delayedEnableCoroutine != null)
            StopCoroutine(_delayedEnableCoroutine);
        _delayedEnableCoroutine = StartCoroutine(IEDelayedEnable());
    }
    private void OnDisable()
    {
        if (!Player) return;
        Player.onMove -= OnMove;
        Player.onNorth -= OnNorth;
        Player.onEast -= OnEast;
        Player.onSouth -= OnSouth;
        Player.onWest -= OnWest;
    }

    private IEnumerator IEDelayedEnable()
    {
        yield return 0; //Wait one frame
        if (Player)
        {
            Player.onMove += OnMove;
            Player.onNorth += OnNorth;
            Player.onEast += OnEast;
            Player.onSouth += OnSouth;
            Player.onWest += OnWest;
        }
    }

    protected virtual void OnMove(InputValue value) { }
    protected virtual void OnNorth(InputValue value) { }
    protected virtual void OnEast(InputValue value) { }
    protected virtual void OnSouth(InputValue value) { }
    protected virtual void OnWest(InputValue value) { }

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
        if (inputLocks < 1)
            OnEnable();
    }
}
