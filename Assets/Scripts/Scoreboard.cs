using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class Scoreboard : MonoBehaviour
{
    private static Scoreboard Instance;

    [Serializable]
    public struct PlayerBoard
    {
        public Image board;
        public TextMeshProUGUI scoreText;
        public int score { get; private set; }
        public Player player;
        public GameObject gameObject { get { return board.gameObject; } }

        public void Init(Player player)
        {
            this.player = player;
            board.color = player.color;
            gameObject.SetActive(true);
        }
        public void Clear()
        {
            board.color = Color.white;
            SetScore(0);
            gameObject.SetActive(false);
        }
        public void SetScore(int score)
        {
            this.score = score;
            scoreText.text = score.ToString();
        }
        public void AddScore(int score)
        {
            SetScore(this.score + score);
        }
    }

    public PlayerBoard[] playerBoards;

    //public Image[] playerBoards;
    //public TextMeshProUGUI[] playerScoreTexts;
    
    //private int[] playerScores;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    private static void ClearBoards()
    {
        foreach(PlayerBoard board in Instance.playerBoards)
        {
            board.Clear();
        }
    }

    public void SetPlayerBoards()
    {
        SetPlayerBoards(GameManager.Players);
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
        Instance.playerBoards[player.Index].Init(player);
    }

    public static void AddScore(Player player, int points)
    {
        for (int i = 0; i < Instance.playerBoards.Length; i++)
        {
            if (Instance.playerBoards[i].player == player)
            {
                Instance.playerBoards[i].AddScore(points);
                break;
            }
        }
    }

    public void CalculateWinningPoints()
    {
        PlayerBoard[] orderedBoards = playerBoards.OrderBy(board => board.score).ToArray();

        int points = 0;

        for (int i = 0; i < orderedBoards.Length; i++)
        {
            if (!orderedBoards[i].player) continue;

            points++;

            Debug.Log(points + " points for: " + orderedBoards[i].score);
            orderedBoards[i].player.AddScore(points);
        }
    }

}
