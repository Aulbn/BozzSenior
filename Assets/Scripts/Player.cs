using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class Player : MonoBehaviour
{
    //[Header("DEBUG")]

    [Header("Stats")]
    public string playerName;
    public Color color;
    public int points;

    private void Start()
    {
        transform.SetParent(GameManager.Instance.transform);
        GameManager.AddPlayer(this);
    }

    public void Disconnect()
    {
        GameManager.RemovePlayer(this);
        StartCoroutine(DelayedDestruction());
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    //--Delegates--//
    public delegate void _OnMove(InputValue value);
    public delegate void _OnNorth(InputValue value);
    public delegate void _OnEast(InputValue value);
    public delegate void _OnSouth(InputValue value);
    public delegate void _OnWest(InputValue value);
    public _OnMove onMove;
    public _OnNorth onNorth;
    public _OnEast onEast;
    public _OnSouth onSouth;
    public _OnWest onWest;

    private void OnMove(InputValue value)
    {
        if (onMove == null) return;
        onMove(value);
    }
    private void OnNorth(InputValue value)
    {
        if (onNorth == null) return;
        onNorth(value);
    }
    private void OnEast(InputValue value)
    {
        if (onEast == null) return;
        onEast(value);
    }
    private void OnSouth(InputValue value)
    {
        if (onSouth == null) return;
        onSouth(value);
    }
    private void OnWest(InputValue value)
    {
        if (onWest == null) return;
        onWest(value);
    }

}
