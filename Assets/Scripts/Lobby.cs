using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public Transform playerContent;
    public GameObject colorSelectPrefab;
    public Transform continueText;

    private static List<PlayerColorSelect> ColorPickers;

    private void Awake()
    {
        ColorPickers = new List<PlayerColorSelect>();
        continueText.gameObject.SetActive(false);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        SpawnColorPicker().Initiate(player.GetComponent<Player>());
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

    public PlayerColorSelect SpawnColorPicker()
    {
        return Instantiate(colorSelectPrefab, playerContent).GetComponent<PlayerColorSelect>();
    }
}
