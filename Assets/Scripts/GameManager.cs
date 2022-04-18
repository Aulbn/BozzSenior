using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Player> players;
    public static bool HasActiveInput { get; private set; }
    [Header("Scene Management")]
    public int CurrentLevel;
    public LevelObject MidboardLevel;
    public LevelObject[] LevelQueue; //Kanske ska vara queue sen, men vill se i inpectorn nu

    [Header("DEBUG")]
    public Color[] colors;

    public static Player[] Players { get { return Instance.players.ToArray(); } }
    public static PlayerController[] Controllers { get {
            List<PlayerController> pcs = new List<PlayerController>();
            foreach (Player p in Players)
                pcs.Add(p.Controller);
            return pcs.ToArray(); } }

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(this);

        HasActiveInput = true;
    }

    public static void AddPlayer(Player player)
    {
        //if (Instance.colors != null && Instance.colors.Length > 0) //DEBUG
        //    player.color = Instance.colors[Instance.players.Count]; //DEBUG
        if (Instance.players == null) Instance.players = new List<Player>();
        Instance.players.Add(player);
    }

    public static void RemovePlayer(Player player)
    {
        Instance.players.Remove(player);
    }

    public static void LoadScene(string sceneName)
    {
        Instance.StartCoroutine(Instance.IESceneLoad(sceneName));
    }

    private IEnumerator IESceneLoad(string sceneName)
    {
        //LoadingScreen.Show();
        yield return StartCoroutine(LoadingScreen.IEToggle(true));

        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
        {
            LoadingScreen.SetProgress(ao.progress);
            yield return null;
        }
        //LoadingScreen.Hide();
        yield return StartCoroutine(LoadingScreen.IEToggle(false));
    }

    public static void LoadNextLevel()
    {
        Instance.CurrentLevel++;
        int adjustedLevelIndex = Instance.CurrentLevel / 2;

        if (Instance.CurrentLevel % 2 == 0)
        {
            //Load midboard
            LoadScene(Instance.MidboardLevel.sceneName);
        }
        else
        {//Load level
            Debug.Log("Current level index: " + Instance.CurrentLevel);
            if (adjustedLevelIndex < Instance.LevelQueue.Length)
                LoadScene(Instance.LevelQueue[adjustedLevelIndex].sceneName);
            else
            {
                //Start last scene
                LoadScene("Lobby");
            }
        }
    }

    public static void SetPlayerControllerInput(bool enableMovement, bool global = false)
    {
        if (global)
        {
            HasActiveInput = enableMovement;
        }
        else
        {
            foreach (PlayerController c in Controllers)
            {
                if (enableMovement)
                    c.RemoveInputLock();
                else
                    c.AddInputLock();
            }
        }
    }

    public static void ReportGameScore(List<Player> playersInPlacementOrder)
    {
        int points = Players.Length;

        for (int i = 0; i < playersInPlacementOrder.Count; i++)
        {
            foreach (Player player in Players)
            {
                if (player == playersInPlacementOrder[i])
                {
                    player.AddScore(points);
                    points--;
                    break;
                }
            }
        }
    }

    public static void ReportGameScore(Vector2Int[] xplayerYpoints)
    {
        int points = Players.Length;
        int prevPoints = xplayerYpoints[0].y;

        for (int i = 0; i < xplayerYpoints.Length; i++)
        {
            //if (!orderedBoards[i].player) continue;
            if (xplayerYpoints[i].y != prevPoints)
                points--;
            Players[xplayerYpoints[i].x].AddScore(points);
            prevPoints = xplayerYpoints[i].y;
        }
    }
}
