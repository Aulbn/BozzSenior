using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour
{
    private Player player;

    private void OnEnable()
    {
        if (!player) return;
        player.onMove += OnMove;
        player.onSouth += OnSubmit;
        player.onEast += OnBack;
    }
    private void OnDisable()
    {
        player.onMove -= OnMove;
        player.onSouth -= OnSubmit;
        player.onEast -= OnBack;
    }

    protected abstract void OnMove(InputValue value);
    protected abstract void OnSubmit(InputValue value);
    protected abstract void OnBack(InputValue value);
}
