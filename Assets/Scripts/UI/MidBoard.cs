using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class MidBoard : MonoBehaviour
{
    public float startGameTime;
    public PlayerScoreCard[] playerCards;
    public UITimer timer;
    public GameObject countdownModule;

    private bool isAllReady = false;

    public void Start()
    {
        SetPlayerCards();
        OrderCards();
    }

    private void Update()
    {
        CheckPlayerReady();

    }

    private void CheckPlayerReady()
    {
        isAllReady = true;
        foreach (PlayerScoreCard card in playerCards)
        {
            if (!card.isReady && card.gameObject.activeSelf)
            {
                isAllReady = false;
                break;
            }
        }

        if (isAllReady)
        {
            if (!timer.isRunning) //Start start-coroutine
            {
                countdownModule.SetActive(true);
                timer.StartTimer(startGameTime, () => { Debug.Log("START GAME"); }); //This is where we start next game
            }
        }
        else
        {
            //Stop start-coroutine
            countdownModule.SetActive(false);
            timer.StopTimer();
        }
    }

    public void SetPlayerCards()
    {
        for (int i = 0; i < playerCards.Length; i++)
        {
            if (i >= GameManager.Players.Length) { playerCards[i].gameObject.SetActive(false);  continue; } //HIde if player does not exist
            playerCards[i].Init(GameManager.Players[i]);
        }
    }

    public void OrderCards()
    {
        PlayerScoreCard[] cardsOrdered = playerCards.OrderByDescending(card => int.Parse(card.scoreText.text)).ToArray(); //Order

        for (int i = 0; i < playerCards.Length; i++) //Move to correct order (we can animate this instead)
        {
            cardsOrdered[i].transform.SetSiblingIndex(i);
        }
    }
}
