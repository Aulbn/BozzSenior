using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerPosition[] spawnPoints;
    public PlayerController playerPrefab;

    //public List<PlayerController> playerControllers = new List<PlayerController>();

    public void SpawnPlayer(Player player)
    {
        PlayerController pc = Instantiate(playerPrefab, spawnPoints[player.Index].position, Quaternion.identity).GetComponent<PlayerController>();
        player.SetController(pc);
        pc.SetPlayer(player);

        spawnPoints[player.Index].MoveToPosition((MoveController)pc);
    }

    public void SpawnAllPlayers()
    {
        foreach(Player p in GameManager.Players)
        {
            SpawnPlayer(p);
        }
    }
}
