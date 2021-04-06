using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private float health;
    public float maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health = Mathf.Clamp(health -= damage, 0, float.MaxValue);
        if (health <= 0)
        {
            PlayerController controller = GetComponent<PlayerController>();
            controller.OnDeath();
            PlayerSpawner.RespawnPlayer(controller);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        //Set score in scoreboard, as well
    }
}
