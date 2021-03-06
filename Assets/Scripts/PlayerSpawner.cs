﻿using System.Collections;
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
        PlayerController pc = Instantiate(playerPrefab, spawnPoints[Mathf.Clamp(player.Index, 0, spawnPoints.Length-1)].position, Quaternion.identity).GetComponent<PlayerController>();
        player.SetController(pc);
        pc.SetPlayer(player);

        //spawnPoints[player.Index].MoveToPosition((MoveController)pc);
        //pc.transform.position = spawnPoints[player.Index].position;
    }

    public void SpawnAllPlayers()
    {
        Debug.Log(GameManager.Players.Length);
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
        playerController.enabled = false;
        yield return new WaitForSecondsRealtime(respawnTime);
        playerController.transform.position = spawnPoints[Mathf.Clamp(playerController.Player.Index, 0, spawnPoints.Length-1)].position;
        playerController.enabled = true;
    }
}
