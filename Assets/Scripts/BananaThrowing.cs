using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaThrowing : MonoBehaviour
{
    public GameObject banana;
    private Rigidbody rb;

    void Start()
    {
        rb = banana.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerMovement.bananaCount > 0) {
            Instantiate(banana, transform.position, transform.rotation);
            PlayerMovement.bananaCount--;
        }
    }
}
