using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrain_TotemPoles : MonoBehaviour
{
    public float actionCooldown;
    public float stunTime;
    public int NTotems;
    public float SpeedMultiplier;

    private List<TotemPole> _TotemPoles = new List<TotemPole>();
    private int _NFinishedPlayers = 0;

    public void StartGame()
    {
        foreach (TotemPole totemPole in _TotemPoles)
        {
            totemPole.Activate(actionCooldown, stunTime);
        }
        TogglePlayerInput(true);
    }

    public void AddTotemPole(TotemPole totemPole)
    {
        _TotemPoles.Add(totemPole);
    }

    public void TogglePlayerInput(bool enable)
    {
        GameManager.TogglePlayerControllerInput(enable);
    }

    public int RegisterFinish()
    {
        _NFinishedPlayers++;
        Debug.Log("Register finish");
        if (_NFinishedPlayers == _TotemPoles.Count)
            StartCoroutine(IEEndGame());
        return _NFinishedPlayers;
    }

    private IEnumerator IEEndGame()
    {
        Debug.Log("End Game!");
        yield return new WaitForSeconds(3);
        GameManager.LoadNextLevel();
    }

}
