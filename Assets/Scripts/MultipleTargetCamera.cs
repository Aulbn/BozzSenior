using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    [Header("Move")]
    public Vector3 offset;
    public float smoothTime;
    [Header("Zoom")]
    public Vector2 minMaxFoV;
    public float zoomSpeed;

    public Camera Camera { get; private set; }

    private Vector3 velocity;
    private List<Transform> targets;

    //private Vector3 _centerPoint;
    //private float _greatestDistance;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        //if (targets == null || targets.Count < 1) return;
        if (GameManager.Players == null || GameManager.Players.Length < 1) return;

        //Bounds
        //Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        Bounds bounds = new Bounds(GameManager.Players[0].Controller.transform.position, Vector3.zero);
        //foreach (Transform t in targets)
        foreach (Player p in GameManager.Players)
        {
            bounds.Encapsulate(p.Controller.transform.position);
        }

        //Move
        transform.position = Vector3.SmoothDamp(transform.position, bounds.center + offset, ref velocity, smoothTime);

        //Zoom
        float newZoom = Mathf.Lerp(minMaxFoV.x, minMaxFoV.y, bounds.size.x / 4);
        Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, newZoom, Time.deltaTime * zoomSpeed);
    }

    //private Vector3 GetCenterPoint()
    //{
    //    Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
    //    foreach(Transform t in targets)
    //    {
    //        bounds.Encapsulate(t.position);
    //    }

    //    return bounds.center;
    //}

    public void AddTarget(Transform transform)
    {
        if (targets == null) targets = new List<Transform>();
        targets.Add(transform);
    }
}
