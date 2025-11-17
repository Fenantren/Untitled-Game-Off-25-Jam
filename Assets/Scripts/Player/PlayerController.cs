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
    [SerializeField] float jumpAcceleration = 5f;
    [SerializeField] float currentSpeed;
    [SerializeField] float slamForce = -20f;

    [SerializeField] bool canSlam = false;
    [SerializeField] bool isGrounded;
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

        IsGrounded();
        
        
        

    }


    void FixedUpdate()
    {
        MovePlayer();
        Look();

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

    void MovePlayer()
    {
        
        rb.MovePosition(transform.position + (transform.forward * input.magnitude) * currentSpeed * Time.deltaTime);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentSpeed = dashSpeed;
        }
        else if (context.canceled)
        {
            currentSpeed = moveSpeed;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if ( isGrounded && context.performed )
        {
            rb.linearVelocity = Vector3.up * jumpForce * jumpAcceleration;
            canSlam = true; 
            
        }

        else if( canSlam && context.performed)
        {
            rb.linearVelocity = -Vector3.up * slamForce;
            canSlam = false;
        }
        
        if ( context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.75f, rb.linearVelocity.z);
        }
    }

    void IsGrounded()
    {
        float radius = 0.2f;
        
        if( Physics.CheckSphere(groundCheck.transform.position, radius, groundLayer))
        {
            isGrounded = true;
            
        }
        else
        {
            isGrounded = false;
            
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        input = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.transform.position, 0.2f);
    }

    
}
