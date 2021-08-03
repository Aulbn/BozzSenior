using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public enum PositionType
    {
        Multiple, Single
    }
    public PositionType positionType;
    [Tooltip("Only matters when using 'Multiple' type.")]
    public float centerOffset = 0.5f;

    public Vector3 position { get { return transform.position; } }
    private PlayerController[] reservations = new PlayerController[4];

    private static List<PlayerPosition> AllPositions;

    private void Awake()
    {
        if (AllPositions == null) AllPositions = new List<PlayerPosition>();
        AllPositions.Add(this);
    }

    public Vector3 GetMultiPosition(PlayerController controller)
    {
        int posIndex;
        if (!Contains(controller)) //Don't have this player
        {
            foreach (PlayerPosition p in AllPositions) //Remove from other position
            {
                if (p == this) continue;
                if (p.CancelReservation(controller))
                    break;
            }

            //Add player and return a free position
            posIndex = GetFreePositionIndex();
            reservations[posIndex] = controller;
        }
        else //Already have this player
        {
            posIndex = GetPlayerIndex(controller);
        }

        return GetMultiPosition(posIndex);        
    }
    public bool CancelReservation(PlayerController controller)
    {
        int playerIndex = GetPlayerIndex(controller);
        if (playerIndex > -1) //Exists
        {
            reservations[playerIndex] = null;
            return true;
        }
        return false;
    }
    public static void ForceCancelReservation(PlayerController controller)
    {
        foreach (PlayerPosition p in AllPositions)
        {
            if (p.CancelReservation(controller))
                break;
        }
    }

    private int GetPlayerIndex(PlayerController controller)
    {
        for (int i = 0; i < reservations.Length; i++)
        {
            if (reservations[i] == controller)
                return i;
        }
        return -1;
    }

    public bool Contains(PlayerController controller)
    {
        return GetPlayerIndex(controller) > -1;
    }

    private int GetFreePositionIndex()
    {
        for (int i = 0; i < reservations.Length; i++)
        {
            if (reservations[i] == null)
                return i;
        }

        return -1; //Out of bounds, should never be reached
    }

    public Vector3 GetMultiPosition(int index)
    {
        switch (index)
        {
            case 0:
                return transform.position + new Vector3(0, 0, 1) * centerOffset;
            case 1:
                return transform.position + new Vector3(1, 0, 0) * centerOffset;
            case 2:
                return transform.position + new Vector3(-1, 0, 0) * centerOffset;
            case 3:
                return transform.position + new Vector3(0, 0, -1) * centerOffset;
            default:
                return position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.green;
        if (positionType == PositionType.Multiple)
        {
            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawRay(GetMultiPosition(i), Vector3.up);
            }
        }
        else
            Gizmos.DrawRay(transform.position, transform.up);
    }

}
