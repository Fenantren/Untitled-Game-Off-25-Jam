using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float turnSpeed = 360f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float currentSpeed;
    [SerializeField] bool isGrounded;
    private Vector3 input;

    PlayerInputs playerInputs;
    PlayerInput playerInput;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private void Start()
    {
        playerInputs = GetComponent<PlayerInputs>();
        playerInput = GetComponent<PlayerInput>();
        currentSpeed = moveSpeed;
    }
    void Update()
    {
        IsGrounded();
        GatherInput();
        Look();
        Dash();
    }


    void FixedUpdate() 
    {
        Move();
        Jump();
    }

    void GatherInput()
    {
        input = new Vector3(playerInputs.move.x, 0, playerInputs.move.y);
    }

    private void Look()
    {
        if(input != Vector3.zero)
        {

            Vector3 relative = (transform.position + input.ToIso()) - transform.position; 
            Quaternion inputRotation = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, inputRotation, turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        rb.MovePosition(transform.position + (transform.forward * input.magnitude) * currentSpeed *Time.deltaTime );
    }

    void Dash()
    {
        if (playerInputs.dash)
        {
            currentSpeed = dashSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    void Jump()
    {
        if ( playerInputs.jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void IsGrounded()
    {
        if (Physics.Raycast(groundCheck.transform.position, Vector3.down, 0.1f, groundLayer, QueryTriggerInteraction.UseGlobal))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
