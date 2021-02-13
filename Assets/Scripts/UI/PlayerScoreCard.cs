using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerScoreCard : PlayerController
{
    [SerializeField] private Image background;
    public TextMeshProUGUI scoreText, nameText;
    public ToggleSwitch toggleSwitch;

    //public bool isReady { get; private set; }
    public bool isReady;

    public void Init(Player player)
    {
        SetPlayer(player);
        background.color = player.color;
        scoreText.text = player.score.ToString();
        nameText.text = player.playerName;
    }

    protected override void OnSouth(InputValue value)
    {
        ToggleReady(!isReady);
    }

    protected override void OnEast(InputValue value)
    {
        ToggleReady(false);
    }

    private void ToggleReady(bool isReady)
    {
        this.isReady = isReady;
        toggleSwitch.Toggle(isReady, .15f);
    }

}
