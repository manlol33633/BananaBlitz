using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool canThrow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000 + transform.up * 250);
    }

    void Update()
    {
        transform.Rotate(0, 10, 0);
    }

    void FixedUpdate()
    {
       
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            Destroy(gameObject);
        }
    }
}
