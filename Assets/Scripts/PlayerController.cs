using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour
{
    public Player Player { get; private set; }

    public void SetPlayer(Player player)
    {
        OnDisable();
        Player = player;
        OnEnable();
    }

    private void OnEnable()
    {
        if (!Player) return;
        Player.onMove += OnMove;
        Player.onNorth += OnNorth;
        Player.onEast += OnEast;
        Player.onSouth += OnSouth;
        Player.onWest += OnWest;
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


    protected virtual void OnMove(InputValue value) { }
    protected virtual void OnNorth(InputValue value) { }
    protected virtual void OnEast(InputValue value) { }
    protected virtual void OnSouth(InputValue value) { }
    protected virtual void OnWest(InputValue value) { }
}
