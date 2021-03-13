using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public GameObject lobbyPlayerControllerPrefab;
    public GameObject outfitListPrefab;
    public Transform contentHolder;
    public Transform continueText;
    public MultipleTargetCamera mainCamera;
    [Space]
    public Transform[] spawnPoints;

    private static List<PlayerColorSelect> ColorPickers;

    private void Awake()
    {
        ColorPickers = new List<PlayerColorSelect>();
        continueText.gameObject.SetActive(false);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        LobbyPlayerController pc = Instantiate(lobbyPlayerControllerPrefab, spawnPoints[GameManager.Players.Length]).GetComponent<LobbyPlayerController>();
        pc.Initiate(player.GetComponent<Player>(), this, Instantiate(outfitListPrefab, contentHolder).GetComponent<UIList>());
        mainCamera.AddTarget(pc.transform);
        Debug.Log(player.user.index + " joined");
    }

    private void Update()
    {
        if (ColorPickers.Count < 1) return;
        foreach (PlayerColorSelect pcs in ColorPickers)
        {
            if (!pcs.isReady)
            {
                continueText.gameObject.SetActive(false);
                return;
            }
        }
        continueText.gameObject.SetActive(true);
    }

    public static void AddColorPicker(PlayerColorSelect pcs)
    {
        ColorPickers.Add(pcs);
    }
    public static void RemoveColorPicker(PlayerColorSelect pcs)
    {
        ColorPickers.Remove(pcs);
    }
}
