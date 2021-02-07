using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private static Scoreboard Instance;

    public Image[] playerBoards;
    public TextMeshProUGUI[] playerScoreTexts;
    
    private int[] playerScores;

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
            SetScore(i, 0);
        }
    }

    public void SetPlayerBoards()
    {
        //Debug.Log(GameManager.Players.Length);
        SetPlayerBoards(GameManager.Players);
    }
    public static void SetPlayerBoards(Player[] players)
    {
        Instance.playerScores = new int[players.Length];
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

    public static void SetScore(int playerIndex, int score)
    {
        if (Instance.playerScores.Length <= playerIndex) return;

        Instance.playerScores[playerIndex] = score;
        Instance.playerScoreTexts[playerIndex].text = score.ToString();
    }

    public static void AddScore(int playerIndex, int points)
    {
        SetScore(playerIndex, Instance.playerScores[playerIndex] + points);
    }


}
