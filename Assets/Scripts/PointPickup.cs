using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickup : MonoBehaviour
{
    public enum PointType
    {
        Normal, Split
    }
    public PointType pointType;
    public int points;

    private List<Player> pointPlayers = new List<Player>();

    private void LateUpdate()
    {
        if (pointType == PointType.Split && pointPlayers.Count > 0)
        {
            foreach(Player p in pointPlayers)
            {
                Scoreboard.AddScore(p.Index, points / pointPlayers.Count);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                switch (pointType)
                {
                    case PointType.Normal:
                        Scoreboard.AddScore(player.Player.Index, points);
                        Destroy(gameObject);
                        break;
                    case PointType.Split:
                        pointPlayers.Add(player.Player);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
