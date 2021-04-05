using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour
{
    public Rigidbody dispenceObject;
    public float dispenseForce;

    public void Use()
    {
        Rigidbody rb = Instantiate(dispenceObject.gameObject, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rb.gameObject.SetActive(true);
        rb.AddForce(transform.forward * dispenseForce);
    }
}
