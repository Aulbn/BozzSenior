﻿using System.Collections;
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

    public int Score { get; private set; }
    public int Index { get { return GetComponent<PlayerInput>().user.index; } } //change if it is called often
    public bool HasHybridModel { get { return hybridModel != null; } }
    private GameObject hybridModel;
    public PlayerController Controller { get; private set; }
    public PlayerActions InputControls { get; private set; }

    private bool _hasInitialized;
    private int _LastShownScore;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_hasInitialized) return;
        transform.SetParent(GameManager.Instance.transform);

        InputControls = new PlayerActions();
        InputControls.devices = GetComponent<PlayerInput>().devices;

        GameManager.AddPlayer(this);

        _hasInitialized = true;
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
        Score += score;
        //Debug.Log("Added score: " + score + " (total: " + Score + ")");
    }

    public int GetScore()
    {
        _LastShownScore = Score;
        return Score;
    }
    public int GetLastShownScore()
    {
        return _LastShownScore;
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

}
