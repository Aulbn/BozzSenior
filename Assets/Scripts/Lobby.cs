using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    //public Transform playerContent;
    //public GameObject colorSelectPrefab;
    public GameObject lobbyPlayerControllerPrefab;
    public Transform lobbySpawn;
    public Transform continueText;
    public RectTransform content;

    private static List<PlayerColorSelect> ColorPickers;
    private Vector2 scrollTargetPosition;

    private void Awake()
    {
        ColorPickers = new List<PlayerColorSelect>();
        continueText.gameObject.SetActive(false);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        Instantiate(lobbyPlayerControllerPrefab, lobbySpawn).GetComponent<LobbyPlayerController>().Initiate(player.GetComponent<Player>(), this);
        //SpawnColorPicker().Initiate(player.GetComponent<Player>());
        Debug.Log(player.user.index + " joined");
    }

    private void Update()
    {
        ButtonScrollUpdate();

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

    public void MoveCursor(int index)
    {
        Debug.Log("!!" + index);
        float height = 100;
        float padding = 10;
        //int nElements = 2;

        float k = (height + padding) * index;
        float m = (content.rect.height / 2) - height/2;

        scrollTargetPosition = new Vector2(0, k - m);

    }

    private void ButtonScrollUpdate()
    {
        content.localPosition = Vector2.Lerp(content.localPosition, scrollTargetPosition, Time.deltaTime * 10f);
    }

    //public PlayerColorSelect SpawnColorPicker()
    //{
    //    return Instantiate(colorSelectPrefab, playerContent).GetComponent<PlayerColorSelect>();
    //}
}
