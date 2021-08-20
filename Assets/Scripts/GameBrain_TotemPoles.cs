using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrain_TotemPoles : MonoBehaviour
{
    public float actionCooldown;
    public float stunTime;

    private List<TotemPole> _TotemPoles = new List<TotemPole>();

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

}
