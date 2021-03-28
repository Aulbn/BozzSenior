using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class Player : MonoBehaviour
{
    [Header("Stats")]
    public string playerName;
    public Color color;
    [SerializeField] private int score;

    public int Score { get { return score; } }
    public int oldScore { get; private set; }
    public int Index { get { return GetComponent<PlayerInput>().user.index; } } //change if it is called often
    public bool HasHybridModel { get { return hybridModel != null; } }
    private GameObject hybridModel;
    public PlayerController Controller { get; private set; }
    
    private PlayerActions actions;

    private void Start()
    {
        transform.SetParent(GameManager.Instance.transform);
        GameManager.AddPlayer(this);

        SetUpInput();
    }

    public void SetController(PlayerController playerController)
    {
        Controller = playerController;
    }

    public void SaveHybridModel(GameObject hybridGameObject)
    {
        hybridModel = Instantiate(hybridGameObject, transform);
        hybridModel.transform.localPosition = Vector3.zero;
        hybridModel.SetActive(false);
    }
    public HybridModel GetHybridModelCopy(Transform parent)
    {
        if (hybridModel == null) return null;
        return Instantiate(hybridModel, parent).GetComponent<HybridModel>();
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

    void shit(InputAction.CallbackContext context)
    {
        Debug.Log("New! " + Index);
    }
    void testprint()
    {
        Debug.Log("Old " + Index);
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    //------------------------INPUT------------------------//
    //--Delegates--//
    public delegate void OnInputValue(InputAction.CallbackContext context);
    public delegate void OnInput();
    public OnInputValue onMove;
    public OnInput onNorth;
    public OnInput onNorthUp;
    public OnInput onEast;
    public OnInput onEastUp;
    public OnInput onSouth;
    public OnInput onSouthUp;
    public OnInput onWest;
    public OnInput onWestUp;

    private void SetUpInput()
    {
        actions = new PlayerActions();
        actions.devices = GetComponent<PlayerInput>().devices;

        actions.Lobby.Move.performed += OnMove;
        actions.Lobby.Move.Enable();

        actions.Lobby.West.performed += OnWest;
        actions.Lobby.West.Enable();
        actions.Lobby.East.performed += OnEast;
        actions.Lobby.East.Enable();
        actions.Lobby.North.performed += OnNorth;
        actions.Lobby.North.Enable();
        actions.Lobby.South.performed += OnSouth;
        actions.Lobby.South.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (onMove == null) return;
        if (context.performed)
            onMove(context);
    }
    public void OnNorth(InputAction.CallbackContext context)
    {
        if (onNorth == null) return;
        if (context.performed)
            onNorth();
        else if (context.canceled)
            onNorthUp();
    }
    public void OnEast(InputAction.CallbackContext context)
    {
        if (onEast == null) return;
        if (context.performed)
            onEast();
        else if (context.canceled)
            onEastUp();
    }
    public void OnSouth(InputAction.CallbackContext context)
    {
        if (onSouth == null) return;
        if (context.performed)
            onSouth();
        else if (context.canceled)
            onSouthUp();
    }
    public void OnWest(InputAction.CallbackContext context)
    {
        if (onWest == null) return;
        if (context.performed)
            onWest();
        else if (context.canceled)
            onWestUp();
    }

}
