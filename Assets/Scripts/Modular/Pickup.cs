using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int score;
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
    
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (score != 0)
                Scoreboard.AddScore(player.Player, score);

            Destroy(gameObject);
        }
    }
}
