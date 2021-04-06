using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditorInternal;

public class Projectile : MonoBehaviour
{
    public PlayerStats owner;
    public enum Tags
    {
        Any, ProjectileTarget
    }
    public Tags collisionTag;
    public UnityEvent onCollision;


    public void OnCollisionEnter(Collision collision)
    {
        if (collisionTag == Tags.Any || collision.collider.gameObject.CompareTag(collisionTag.ToString()))
            onCollision.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
