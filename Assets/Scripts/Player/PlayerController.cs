using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCollider;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float turnSpeed = 360f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float currentSpeed;
    
    
    private Vector3 input;

    PlayerInputs playerInputs;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        
        currentSpeed = moveSpeed;
    }
    void Update()
    {
        Jump();
        
        GatherInput();
        Look();
        Dash();
        
    }


    void FixedUpdate() 
    {
        Move();
        

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
        if ( IsGrounded() && playerInputs.jump)
        {
            rb.linearVelocity = Vector3.up * jumpForce;
           
        }

    }

    bool IsGrounded()
    {
        float radius = 0.2f;
        
        if( Physics.CheckSphere(groundCheck.transform.position, radius, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.2f);
    }
}
