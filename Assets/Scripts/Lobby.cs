using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public float readyStartTime;
    [Space]
    public GameObject lobbyPlayerControllerPrefab;
    public GameObject outfitListPrefab;
    public GameObject readyTogglePrefab;
    [Space]
    public Transform contentHolder;
    public MultipleTargetCamera mainCamera;
    public UITimer countdownTimer;
    public GameObject countdownModule;
    public Transform[] spawnPoints;

    private List<ToggleSwitch> _readySwitches;

    private void Awake()
    {
        _readySwitches = new List<ToggleSwitch>();
        countdownModule.SetActive(false);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        LobbyPlayerController pc = Instantiate(lobbyPlayerControllerPrefab, spawnPoints[GameManager.Players.Length]).GetComponent<LobbyPlayerController>();
        ToggleSwitch readySwitch = Instantiate(readyTogglePrefab, contentHolder).GetComponent<ToggleSwitch>();
        _readySwitches.Add(readySwitch);

        pc.Initiate(player.GetComponent<Player>(), this, 
            Instantiate(outfitListPrefab, contentHolder).GetComponent<UIList>(),
            readySwitch);
        mainCamera.AddTarget(pc.transform);
        Debug.Log(player.user.index + " joined");
    }

    private void Update()
    {
        bool isAllReady = true;
        if (_readySwitches.Count < 1) return;
        foreach (ToggleSwitch toggle in _readySwitches)
        {
            if (!toggle.isOn)
            {
                isAllReady = false;
                break;
            }
        }
        countdownModule.SetActive(true);

        if (isAllReady)
        {
            if (!countdownTimer.isRunning) //Start start-coroutine
            {
                countdownModule.SetActive(true);
                countdownTimer.StartTimer(readyStartTime, () => { Debug.Log("START GAME"); SaveAllHybridModels(); GameManager.LoadNextLevel(); }); //This is where we start next game
            }
        }
        else
        {
            //Stop start-coroutine
            countdownModule.SetActive(false);
            countdownTimer.StopTimer();
        }
    }

    private void SaveAllHybridModels()
    {
        foreach (Player p in GameManager.Players)
        {
            ((LobbyPlayerController)p.Controller).SaveHybridCharacter();
        }
    }
}
