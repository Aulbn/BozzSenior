using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public enum PositionTypeEnum
    {
        Single, MultipleRadius, MultipleLine
    }
    public PositionTypeEnum PositionType;
    [Tooltip("Only matters when using a 'Multiple' type.")]
    public float offset = 0.5f;

    public Vector3 position { get { return transform.position; } }
    private PlayerController[] reservations = new PlayerController[4];

    private static List<PlayerPosition> AllPositions;

    private void OnEnable()
    {
        if (AllPositions == null) AllPositions = new List<PlayerPosition>();
        AllPositions.Add(this);
    }

    private void OnDisable()
    {
        AllPositions.Remove(this);
    }

    public bool GetAndBookPosition(PlayerController controller, out Vector3 position)
    {
        int posIndex;
        position = transform.position;

        if (PositionType == PositionTypeEnum.Single)
        {
            if (reservations[0] == null)
            {
                return true;
            }
            else
            {
                if (reservations[0] == controller)
                    return true;
                return false;
            }
        }
        else //Multiple will always have room for all (4) players.
        {
            if (!Contains(controller)) //Don't have this player
            {
                foreach (PlayerPosition p in AllPositions) //Remove from other position
                {
                    if (p == this) continue;
                    if (p.CancelReservation(controller))
                        break;
                }

                //Add player
                posIndex = GetFreePositionIndex();
                reservations[posIndex] = controller;
            }
            else //Already have this player
            {
                posIndex = GetPlayerIndex(controller);
            }

            //Return a position
            if (PositionType == PositionTypeEnum.MultipleRadius)
                position = GetMultipleRadiusPosition(posIndex); 
            else if (PositionType == PositionTypeEnum.MultipleLine)
                position = GetMultipleLinePosition(posIndex);
            return true;
        }
    }
    private Vector3 GetMultipleRadiusPosition(int index)
    {
        switch (index)
        {
            case 0:
                return transform.position + new Vector3(0, 0, 1) * offset;
            case 1:
                return transform.position + new Vector3(1, 0, 0) * offset;
            case 2:
                return transform.position + new Vector3(-1, 0, 0) * offset;
            case 3:
                return transform.position + new Vector3(0, 0, -1) * offset;
            default:
                return position;
        }
    }
    private Vector3 GetMultipleLinePosition(int index)
    {
        switch (index)
        {
            case 0:
                return transform.position + transform.forward * offset;
            case 1:
                return transform.position + transform.forward * 3 * offset;
            case 2:
                return transform.position + -transform.forward * offset;
            case 3:
                return transform.position + -transform.forward * 3 * offset;
            default:
                return position;
        }
    }

    private bool CancelReservation(PlayerController controller)
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

    public bool IsAvailable { get {
            for (int i = 0; i < reservations.Length; i++)
            {
                if (reservations[i] != null)
                    return true;
            }
            return false;
        } }

    private int GetFreePositionIndex()
    {
        for (int i = 0; i < reservations.Length; i++)
        {
            if (reservations[i] == null)
                return i;
        }

        return -1; //Out of bounds, should never be reached
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.green;

        switch (PositionType)
        {
            case PositionTypeEnum.MultipleRadius:
                for (int i = 0; i < 4; i++)
                {
                    Gizmos.DrawRay(GetMultipleRadiusPosition(i), Vector3.up);
                }
                break;
            case PositionTypeEnum.MultipleLine:
                for (int i = 0; i < 4; i++)
                {
                    Gizmos.DrawRay(GetMultipleLinePosition(i), Vector3.up);
                }
                break;
            default:
                Gizmos.DrawRay(transform.position, transform.up);
                break;
        }
    }

}
