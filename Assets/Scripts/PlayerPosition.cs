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
    private List<MoveController> controllers = new List<MoveController>();

    public void AddController(MoveController controller)
    {
        if (!controllers.Contains(controller))
        {
            controller.SetPosition(this);
            controllers.Add(controller);
        }
    }
    public void RemoveController(MoveController controller)
    {
        controllers.Remove(controller);
    }

    public void MoveToPosition(MoveController controller)
    {
        AddController(controller);
        Debug.Log("How many? " + controllers.Count);
        for (int i = 0; i < controllers.Count; i++)
        {
            controller.MoveToPosition(GetMultiPosition(i), MoveController.TravelType.Teleport); //Move every controller on this position (PlayerPosition)
        }
    }

    public Vector3 GetMultiPosition(int index)
    {
        Vector3 pos = transform.position;

        switch (index)
        {
            case 0:
                return pos = transform.position + new Vector3(1, 0, 1) * centerOffset;
            case 1:
                return pos = transform.position + new Vector3(1, 0, -1) * centerOffset;
            case 2:
                return pos = transform.position + new Vector3(-1, 0, 1) * centerOffset;
            case 3:
               return pos = transform.position + new Vector3(-1, 0, -1) * centerOffset;
            default:
                return pos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
    }

}
