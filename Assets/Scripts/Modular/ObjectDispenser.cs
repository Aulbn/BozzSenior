using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour
{
    public Rigidbody dispenceObject;
    public float dispenseForce;
    public float cooldown;
    private float cooldownTime;
    public void Use()
    {
        if (Time.time < cooldownTime) return;

        Rigidbody rb = Instantiate(dispenceObject.gameObject, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rb.gameObject.SetActive(true);
        rb.AddForce(transform.forward * dispenseForce);

        cooldownTime = Time.time + cooldown;
    }
}
