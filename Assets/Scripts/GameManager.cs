using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Player> players;

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
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        LoadingScreen.Show();
        while (!ao.isDone)
        {
            LoadingScreen.SetProgress(ao.progress);
            yield return null;
        }
        LoadingScreen.Hide();
    }
}
