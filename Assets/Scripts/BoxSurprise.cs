using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxSurprise : MonoBehaviour
{
    public PlayerSpawner spawner;
    public GameObject pricePrefab, penaltyPrefab;
    public float roundTime = 6f;
    public int nRounds = 3;

    [Header("Ref")]
    public Transform[] boxes;

    private void Start()
    {
        Scoreboard.SetPlayerBoards(GameManager.Players);
        spawner.SpawnAllPlayers();
        foreach (MoveController mc in GameManager.Controllers)
        {
            mc.positions = new PlayerPosition[4];
            for (int i = 0; i < 4; i++)
            {
                mc.positions[i] = spawner.SpawnPoints[i];
            }
        }
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
        //Scoreboard.SetClockColor(Color.yellow);
        yield return new WaitForSeconds(3);

        for (int i = 0; i < nRounds; i++)
        {
            //Start new round
            ToggleMovement(true);
            timer = 0;
            //Scoreboard.SetClockColor(Color.green);
            while (timer < roundTime)
            {
                timer += Time.deltaTime;
                //Scoreboard.SetTime(1 - (timer / roundTime));
                yield return new WaitForEndOfFrame();
            }
            //Round over
            ToggleMovement(false);
            //Scoreboard.SetClockColor(Color.yellow);
            //Scoreboard.SetTime(1);
            yield return new WaitForSeconds(3);
        }
        //Game over
        //Scoreboard.SetClockColor(Color.red);
        yield return new WaitForSeconds(3);
        GameManager.LoadScene("Lobby");
    }

    private void ToggleMovement(bool enabled)
    {
        foreach(Player p in GameManager.Players)
        {
            ((MoveController)p.Controller).canMove = enabled;
        }
    }

}
