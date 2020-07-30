using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxSurprise : MonoBehaviour
{
    public GameObject pricePrefab, penaltyPrefab;
    public float roundTime = 6f;
    public int nRounds = 3;
    public bool canMove;

    [Header("Ref")]
    public Transform[] boxes;
    public Image clock;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        float timer;

        //Intro
        clock.color = Color.yellow;
        yield return new WaitForSeconds(3);

        for (int i = 0; i < nRounds; i++)
        {
            //Start new round
            timer = 0;
            canMove = true;
            clock.color = Color.green;
            while (timer < roundTime)
            {
                timer += Time.deltaTime;
                clock.fillAmount = 1 - (timer / roundTime);
                yield return new WaitForEndOfFrame();
            }
            //Round over
            canMove = true;
            clock.color = Color.yellow;
            clock.fillAmount = 1;
            yield return new WaitForSeconds(3);
        }
        //Game over
        clock.color = Color.red;
        yield return new WaitForSeconds(3);
        GameManager.LoadScene("Lobby");
    }

}
