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
    private List<Player> _FinishedPlayers = new List<Player>();

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
        GameManager.SetPlayerControllerInput(enable);
    }

    public int RegisterFinish(Player player)
    {
        _FinishedPlayers.Add(player);
        Debug.Log("Register finish");
        if (_FinishedPlayers.Count == _TotemPoles.Count)
            StartCoroutine(IEEndGame());
        return _FinishedPlayers.Count;
    }

    private IEnumerator IEEndGame()
    {
        GameManager.ReportGameScore(_FinishedPlayers);
        Debug.Log("End Game!");
        yield return new WaitForSeconds(3);
        GameManager.LoadNextLevel();
    }

}
