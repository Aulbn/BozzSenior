using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrain_HitSame : MonoBehaviour
{
    public float CardWaitTime;
    [Range(0, 1)] public float ChanceOfEqual;
    public bool CanHit;

    public MeshRenderer Card1, Card2;

    //private int _NUnequals;

    [Header("Debug")]
    public Color[] Colors;

    private void Start()
    {
        Card1.enabled = false;
        Card2.enabled = false;
    }

    public void StartPlacingCards()
    {
        CanHit = true;
        StartCoroutine(IEStartPlacingCards());
    }

    private IEnumerator IEStartPlacingCards()
    {
        while (true)
        {
            PlaceCard();
            yield return new WaitForSeconds(CardWaitTime);
        }
    }

    private void PlaceCard()
    {
        MeshRenderer card;
        if (Card1.enabled && Card2.enabled)
            card = Random.Range(0, 2) == 0 ? Card1 : Card2;
        else
            card = Card1.enabled ? Card2 : Card1;
        card.enabled = true;

        if (Random.Range (0f,1f) < ChanceOfEqual && Card1.enabled && Card2.enabled) //Guarantee Equal
        {
            card.material.SetColor("_BaseColor", (card == Card1 ? Card2 : Card1).material.GetColor("_BaseColor"));
        }
        else //Set random color
        {
            int cardIcon = Random.Range(0, Colors.Length);
            card.material.SetColor("_BaseColor", Colors[cardIcon]);
        }
    }

    private bool IsSameCards()
    {
        return Card1.enabled && Card2.enabled && Card1.material.GetColor("_BaseColor") == Card2.material.GetColor("_BaseColor");
    }

    public void HitCard(PlayerController controller)
    {
        if (!CanHit) return;
        CanHit = false;
        bool isSame = IsSameCards();
        Debug.Log(controller.Player.playerName + " hit the table!");
        if (isSame)
        {
            controller.HybridModel.AnimationHandler.PlayAnimation("Headbutt");
            Scoreboard.AddScore(controller.Player, 1);
        }
        else
            controller.HybridModel.AnimationHandler.PlayAnimation("Stunned");

        StopAllCoroutines();
        StartCoroutine(IEHitCard(isSame));
    }

    private IEnumerator IEHitCard(bool isSame)
    {
        yield return new WaitForSeconds(1);

        if (isSame)
        {
            Card1.enabled = false;
            Card2.enabled = false;
            yield return new WaitForSeconds(3);
        }
        StartPlacingCards();
    }

    public void EndGame()
    {
        //Save scores

        GameManager.LoadNextLevel();
    }

}
