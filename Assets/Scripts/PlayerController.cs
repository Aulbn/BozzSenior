using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour
{
    private Player player;

    public void SetPlayer(Player player)
    {
        OnDisable();
        this.player = player;
        OnEnable();
    }

    private void OnEnable()
    {
        if (!player) return;
        player.onMove += OnMove;
        player.onNorth += OnNorth;
        player.onEast += OnEast;
        player.onSouth += OnSouth;
        player.onWest += OnWest;
    }
    private void OnDisable()
    {
        if (!player) return;
        player.onMove -= OnMove;
        player.onNorth -= OnNorth;
        player.onEast -= OnEast;
        player.onSouth -= OnSouth;
        player.onWest -= OnWest;
    }


    protected virtual void OnMove(InputValue value) { }
    protected virtual void OnNorth(InputValue value) { }
    protected virtual void OnEast(InputValue value) { }
    protected virtual void OnSouth(InputValue value) { }
    protected virtual void OnWest(InputValue value) { }
}
