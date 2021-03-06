using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class MidBoard : MonoBehaviour
{
    [Header("Score Cards")]
    public List<PlayerScoreCard> playerCards;
    public GameObject cardPrefab;
    public Transform content;
    [Header("Timer")]
    public float startGameTime;
    public UITimer timer;
    public GameObject countdownModule;
    
    private bool isAllReady = false;

    public void Start()
    {
        CreateCards();
        StartCoroutine(IEIntro());
    }

    private void Update()
    {
        CheckPlayerReady();
    }

    private void CreateCards()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        playerCards.Clear();

        foreach (Player player in GameManager.Players)
        {
            PlayerScoreCard card = Instantiate(cardPrefab, content).GetComponent<PlayerScoreCard>();
            card.Init(player);
            playerCards.Add(card);
        }
    }

    private IEnumerator IEIntro()
    {
        OrderCards(true);
        yield return new WaitForSeconds(3);

        UpdateCardScore();
        yield return new WaitForSeconds(0.5f);

        foreach (PlayerScoreCard card in playerCards)
            card.ToggleSmoothFollow(true);
        OrderCards(false);
        yield return new WaitForSeconds(1);

        foreach (PlayerScoreCard card in playerCards)//Unlock ready toggle switch
        {
            card.ShowToggleSwitch();
        }
    }

    private void CheckPlayerReady()
    {
        isAllReady = true;
        foreach (PlayerScoreCard card in playerCards)
        {
            if (!card.isReady)
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
                timer.StartTimer(startGameTime, () => { Debug.Log("START GAME"); GameManager.LoadNextLevel(); }); //This is where we start next game
            }
        }
        else
        {
            //Stop start-coroutine
            countdownModule.SetActive(false);
            timer.StopTimer();
        }
    }
    private void UpdateCardScore()
    {
        foreach (PlayerScoreCard card in playerCards)
        {
            card.AddPoints(card.Player.score - card.Player.oldScore, .1f);
        }
    }

    private void OrderCards(bool oldScore)
    {
        //PlayerScoreCard[] cardsOrdered = playerCards.OrderByDescending(card => int.Parse(card.scoreText.text)).ToArray(); //Order
        PlayerScoreCard[] cardsOrdered = playerCards.OrderByDescending(card => oldScore ? card.Player.oldScore : card.Player.score).ToArray(); //Order

        for (int i = 0; i < playerCards.Count; i++) //Move to correct order (we can animate this instead)
        {
            cardsOrdered[i].transform.SetSiblingIndex(i);
        }
    }

    //private PlayerScoreCard[] GetOrderedCards()
    //{
    //    return playerCards.OrderByDescending(card => int.Parse(card.scoreText.text)).ToArray();
    //}
}
