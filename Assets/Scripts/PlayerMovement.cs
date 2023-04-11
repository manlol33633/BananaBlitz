using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump = true;

    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float verticalInput;
    private float horizontalInput;

    private Vector3 movementDirection;

    private Rigidbody rb;
    private TMP_Text speedText;

    public MovementState state;

    private int grassBladeCount = 0;
    private int bananaCount = 0;

    private TMP_Text grassBladeText;
    private TMP_Text bananaText;

    public enum MovementState {
        walking,
        sprinting, 
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        speedText = GameObject.Find("SpeedText").GetComponent<TMP_Text>();
        startYScale = transform.localScale.y;
    }

    private void Update () {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            rb.drag = groundDrag;
            Debug.Log("Grounded");
        }
        else
        {
            rb.drag = 0f;
        }

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jumpKey) && grounded && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke("ResetJump", jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movementDirection != Vector3.zero)
        {
            orientation.transform.forward = movementDirection;
        }

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            rb.velocity = flatVelocity.normalized * movementSpeed + Vector3.up * rb.velocity.y;
        }

        speedText.text = "Speed: " + flatVelocity.magnitude;

        if (Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            movementSpeed = crouchSpeed;
        }

        if (Input.GetKey(sprintKey) && grounded)
        {
            movementSpeed = sprintSpeed;
            state = MovementState.sprinting;
        }
        else if (grounded)
        {
            movementSpeed = walkSpeed;
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void FixedUpdate() {
        movementDirection = orientation.transform.forward * verticalInput + orientation.transform.right * horizontalInput;

        if (grounded) {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        } else  if (!grounded) {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void ResetJump() {
        canJump = true;
    }
}
