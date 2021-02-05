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
            PlayerSpawner.RespawnPlayer(GetComponent<PlayerController>());
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        //Set score in scoreboard, as well
    }

    //public float zoomSpeed;
    //private Vector3 newPos;


    //private void Awake()
    //{
    //    newPos = transform.position;
    //}

    //public void ZoomUpdate()
    //{
    //    newPos += transform.forward * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
    //    transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * zoomSpeed);
    //}

}
