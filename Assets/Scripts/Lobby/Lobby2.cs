using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Lobby2 : MonoBehaviour
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

    [HideInInspector] public int nOReadySwitches;


    public void OnPlayerJoined(PlayerInput player)
    {
        //player.GetComponent<Player>().Init();
        //LobbyPlayerController2 pc = Instantiate(lobbyPlayerControllerPrefab, spawnPoints[GameManager.Players.Length]).GetComponent<LobbyPlayerController2>();
        //ToggleSwitch readySwitch = Instantiate(readyTogglePrefab, contentHolder).GetComponent<ToggleSwitch>();
        ////_readySwitches.Add(readySwitch);

        //pc.Initiate(player.GetComponent<Player>(), this,
        //    Instantiate(outfitListPrefab, contentHolder).GetComponent<UIList>(),
        //    readySwitch);
        //mainCamera.AddTarget(pc.transform);
        //Debug.Log(player.user.index + " joined");
    }
}
