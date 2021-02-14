using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrain_Sinking : MonoBehaviour
{
    public PlayerSpawner spawner;
    public Transform[] positions;

    public void Start()
    {

    }

    public void Init()
    {
        spawner.SpawnAllPlayers();

        foreach (JumpController mc in GameManager.Controllers)
        {
            mc.positions = positions;
        }
    }

    public void LoadNextLevel()
    {
        GameManager.LoadNextLevel();
    }

}
