using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrain_Sinking : MonoBehaviour
{
    public PlayerSpawner spawner;
    public PlayerPosition[] positions;

    public SinkingPillar[] pillars;
    public ObjectSpawner[] objectSpawners;

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

    public void ToggleGameFunction(bool isOn)
    {
        foreach (SinkingPillar pillar in pillars)
            pillar.isActive = isOn;
        foreach (ObjectSpawner spawner in objectSpawners)
            spawner.isSpawning = isOn;
    }

    public void TogglePlayerMovement(bool enableMovement)
    {
        foreach(PlayerController c in GameManager.Controllers)
        {
            if (enableMovement)
                c.RemoveInputLock();
            else
                c.AddInputLock();
        }
    }

}
