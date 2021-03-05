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
        public RectTransform rectTransform { get { return (RectTransform)board.transform; } }

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
        public override String ToString()
        {
            return player.playerName + ": " + score;
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
                Tweener.Pulse(Instance, Instance.playerBoards[i].rectTransform, 1.2f, .2f, () => { });
                break;
            }
        }
    }

    public void CelebrateLeaders()
    {
        foreach (PlayerBoard board in GetLeaders())
        {
            Tweener.Pulse(this, board.rectTransform, 2, 3f, () => { });
        }
    }

    public PlayerBoard[] GetLeaders()
    {
        PlayerBoard[] orderedBoards = playerBoards.OrderByDescending(board => board.score).ToArray();
        int nLeaders = 1;
        for (int i = 1; i < orderedBoards.Length; i++)
        {
            if (orderedBoards[i].score != orderedBoards[i - 1].score)
                break;
            nLeaders++;
        }
        PlayerBoard[] newArray = new PlayerBoard[nLeaders];
        Array.Copy(orderedBoards, newArray, nLeaders);
        return newArray;
    }

    public void CalculateWinningPoints()
    {
        PlayerBoard[] orderedBoards = playerBoards.OrderByDescending(board => board.score).ToArray();

        int points = GameManager.Players.Length;
        int prevScore = orderedBoards[0].score;

        for (int i = 0; i < orderedBoards.Length; i++)
        {
            if (!orderedBoards[i].player) continue;
            if (orderedBoards[i].score != prevScore)
                points--;
            orderedBoards[i].player.AddScore(points);
            prevScore = orderedBoards[i].score;
        }
    }

}
