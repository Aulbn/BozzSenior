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
    public int score;
    public int oldScore { get; private set; }
    public int Index { get { return GetComponent<PlayerInput>().user.index; } } //change if it is called often
    //public int Index { get { return index >= 0 ? index : (index = GetComponent<PlayerInput>().user.index); } }
    //private int index = -1;
    public PlayerController Controller { get; private set; }

    private void Start()
    {
        transform.SetParent(GameManager.Instance.transform);
        GameManager.AddPlayer(this);
    }

    public void SetController(PlayerController playerController)
    {
        Controller = playerController;
    }

    public void Disconnect()
    {
        GameManager.RemovePlayer(this);
        StartCoroutine(DelayedDestruction());
    }

    public void AddScore(int score)
    {
        oldScore = this.score;
        this.score += score;
    }


    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    //--Delegates--//
    public delegate void OnInput(InputValue value);
    public OnInput onMove;
    public OnInput onNorth;
    public OnInput onEast;
    public OnInput onSouth;
    public OnInput onWest;

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
