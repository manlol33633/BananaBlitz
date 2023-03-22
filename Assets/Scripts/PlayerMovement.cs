using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public Transform orientation;

    private float verticalInput;
    private float horizontalInput;

    private Vector3 movementDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update () {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movementDirection != Vector3.zero)
        {
            orientation.transform.forward = movementDirection;
        }
    }

    private void FixedUpdate() {
        movementDirection = orientation.transform.forward * verticalInput + orientation.transform.right * horizontalInput;

        rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }
}
