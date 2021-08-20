using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Player> players;
    [Header("Scene Management")]
    public int currentLevel;
    public string midboardSceneName;
    public LevelObject[] levelQueue; //Kanske ska vara queue sen, men vill se i inpectorn nu

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
        Instance.currentLevel++;
        int adjustedLevelIndex = Instance.currentLevel / 2;

        if (Instance.currentLevel % 2 == 0)
        {
            //Load midboard
            LoadScene(Instance.midboardSceneName);
        }
        else
        {//Load level
            Debug.Log("Current level index: " + Instance.currentLevel);
            if (adjustedLevelIndex < Instance.levelQueue.Length)
                LoadScene(Instance.levelQueue[adjustedLevelIndex].sceneName);
            else
            {
                //Start last scene
                LoadScene("Lobby");
            }
        }
    }

    public static void TogglePlayerControllerInput(bool enableMovement)
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
