using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private static PlayerSpawner Instance;

    public float respawnTime;
    [Space]
    public PlayerPosition[] spawnPoints;
    public PlayerController playerPrefab;

    //public List<PlayerController> playerControllers = new List<PlayerController>();

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnPlayer(Player player)
    {
        PlayerController pc = Instantiate(playerPrefab, spawnPoints[player.Index].position, Quaternion.identity).GetComponent<PlayerController>();
        player.SetController(pc);
        pc.SetPlayer(player);

        //spawnPoints[player.Index].MoveToPosition((MoveController)pc);
        pc.transform.position = spawnPoints[player.Index].position;
    }

    public void SpawnAllPlayers()
    {
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
        Instance.StartCoroutine(Instance.IERespawnPlayer(playerController, Instance.respawnTime));
    }
    private IEnumerator IERespawnPlayer(PlayerController playerController, float respawnTime)
    {
        playerController.transform.Translate(Vector3.up * 10000);
        yield return new WaitForSecondsRealtime(respawnTime);
        playerController.transform.position = spawnPoints[playerController.Player.Index].position;
    }
}
