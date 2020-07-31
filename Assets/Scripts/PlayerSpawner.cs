using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public PlayerController playerPrefab;

    //public List<PlayerController> playerControllers = new List<PlayerController>();

    public void SpawnPlayer(Player player)
    {
        PlayerController pc = Instantiate(playerPrefab, spawnPoints[player.Index].position, Quaternion.identity).GetComponent<PlayerController>();
        player.SetController(pc);
        pc.SetPlayer(player);
    }

    public void SpawnAllPlayers()
    {
        foreach(Player p in GameManager.Players)
        {
            SpawnPlayer(p);
        }
    }
}
