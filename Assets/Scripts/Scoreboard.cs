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
        public GameObject Content;
        public Image Background;
        public TextMeshProUGUI ScoreText;
        public int Score { get; private set; }
        [HideInInspector] public Player Player;
        //public GameObject GameObject { get { return Background.gameObject; } }
        public RectTransform RectTransform { get { return (RectTransform)Background.transform; } }

        public void Init(Player player)
        {
            this.Player = player;
            Background.color = player.color;
            Content.SetActive(true);
        }
        public void Clear()
        {
            Background.color = Color.white;
            SetScore(0);
            Content.SetActive(false);
        }
        public void SetScore(int score)
        {
            this.Score = score;
            ScoreText.text = score.ToString();
        }
        public void AddScore(int score)
        {
            SetScore(this.Score + score);
        }
        public override string ToString()
        {
            return Player.playerName + ": " + Score;
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
            if (Instance.playerBoards[i].Player == player)
            {
                Instance.playerBoards[i].AddScore(points);
                Tweener.Pulse(Instance, Instance.playerBoards[i].RectTransform, 1.2f, .2f, () => { });
                //Color originalColor = Instance.playerBoards[i].board.color;
                //Tweener.CrossFadeColor(Instance, Instance.playerBoards[i].board, Color.white, .1f, () => { 
                //    Tweener.CrossFadeColor(Instance, Instance.playerBoards[i].board, originalColor, .1f, () => { });
                //}); //If stuck at white it means it probably picked multiple points at the same time. Known bug, fix later
                break;
            }
        }
    }

    public void CelebrateLeaders()
    {
        foreach (PlayerBoard board in GetLeaders())
        {
            Tweener.Pulse(this, board.RectTransform, 2, 3f, () => { });
        }
    }

    public PlayerBoard[] GetLeaders()
    {
        PlayerBoard[] orderedBoards = playerBoards.OrderByDescending(board => board.Score).ToArray();
        int nLeaders = 1;
        for (int i = 1; i < orderedBoards.Length; i++)
        {
            if (orderedBoards[i].Score != orderedBoards[i - 1].Score)
                break;
            nLeaders++;
        }
        PlayerBoard[] newArray = new PlayerBoard[nLeaders];
        Array.Copy(orderedBoards, newArray, nLeaders);
        return newArray;
    }

    public void CalculateWinningPoints()
    {
        PlayerBoard[] orderedBoards = playerBoards.OrderByDescending(board => board.Score).ToArray();

        //int points = GameManager.Players.Length;
        //int prevScore = orderedBoards[0].score;

        //for (int i = 0; i < orderedBoards.Length; i++)
        //{
        //    if (!orderedBoards[i].player) continue;
        //    if (orderedBoards[i].score != prevScore)
        //        points--;
        //    orderedBoards[i].player.AddScore(points);
        //    prevScore = orderedBoards[i].score;
        //}

        List<Vector2Int> orderedScores = new List<Vector2Int>();
        foreach (PlayerBoard board in orderedBoards)
        {
            if (board.Player != null)
                orderedScores.Add(new Vector2Int(board.Player.Index, board.Score));
        }

        //Debug.Log("nPlayers: " + orderedPlayers.Count);
        GameManager.ReportGameScore(orderedScores.ToArray());

    }

}
