using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private static Scoreboard Instance;

    public Image clock;
    public Image[] playerBoards;
    public TextMeshProUGUI[] playerScores;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    private static void ClearBoards()
    {
        for(int i = 0; i < Instance.playerBoards.Length; i++)
        {
            Instance.playerBoards[i].color = Color.white;
            Instance.playerBoards[i].gameObject.SetActive(false);
            SetScore(i, 0, false);
        }
    }

    public static void SetPlayerBoards(Player[] players)
    {
        ClearBoards();
        foreach(Player p in players)
        {
            SetPlayerBoard(p);
        }
    }

    public static void SetPlayerBoard(Player player)
    {
        Instance.playerBoards[player.Index].gameObject.SetActive(true);
        Instance.playerBoards[player.Index].color = player.color;
    }

    public static void SetTime (float normalized)
    {
        Instance.clock.fillAmount = normalized;
    }
    public static void SetClockColor(Color color)
    {
        Instance.clock.color = color;
    }

    public static void SetScore(int playerIndex, int score, bool transition)
    {
        Instance.playerScores[playerIndex].text = score.ToString();
    }


}
