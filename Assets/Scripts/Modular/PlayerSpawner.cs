using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private static PlayerSpawner Instance;

    public float RespawnTime;
    [Space]
    public PlayerPosition[] SpawnPoints;
    public PlayerController PlayerPrefab;

    //public List<PlayerController> playerControllers = new List<PlayerController>();

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnPlayer(Player player)
    {
        int spawnPointIndex = Mathf.Min(player.Index, SpawnPoints.Length - 1);
        PlayerController pc = Instantiate(PlayerPrefab, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].transform.rotation).GetComponent<PlayerController>();
        pc.gameObject.SetActive(true);
        player.SetController(pc);
        pc.SetPlayer(player);
        pc.SpawnHybridModel();

        SpawnPoints[Mathf.Min(player.Index, SpawnPoints.Length - 1)].GetAndBookPosition(pc, out var pos);
        pc.transform.position = pos;

        //spawnPoints[player.Index].MoveToPosition((MoveController)pc);
        //pc.transform.position = spawnPoints[player.Index].position;
    }

    public void SpawnAllPlayers()
    {
        Debug.Log("Spawn all players: " + GameManager.Players.Length);
        foreach(Player p in GameManager.Players)
        {
            SpawnPlayer(p);
        }
    }

    public static void RespawnPlayer(PlayerController playerController, float respawnTime)
    {
        Instance.StartCoroutine(Instance.IERespawnPlayer(playerController, respawnTime));
    }
    public static void RespawnPlayer(PlayerController playerController)
    {
        Instance.StartCoroutine(Instance.IERespawnPlayer(playerController, Instance.RespawnTime));
    }
    private IEnumerator IERespawnPlayer(PlayerController playerController, float respawnTime)
    {
        playerController.transform.Translate(Vector3.up * 10000);
        playerController.enabled = false;
        yield return new WaitForSecondsRealtime(respawnTime);
        playerController.transform.position = SpawnPoints[Mathf.Clamp(playerController.Player.Index, 0, SpawnPoints.Length-1)].position;
        playerController.enabled = true;
    }
}
