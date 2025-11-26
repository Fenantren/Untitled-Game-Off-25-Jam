using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCollider;
    TrailRenderer trailRenderer;
    [Header ("Move Variables") ]
    [SerializeField] float moveSpeed = 5f;
    
    [SerializeField] float turnSpeed = 360f;

    [Header ("Jump Variables")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpAcceleration = 5f;
    [SerializeField] float currentSpeed;
    [SerializeField] float slamForce = -20f;
    [SerializeField] float fallGravity = 5f;


    [Header("Dashing")]
    [SerializeField] float dashVelocity = 10f;
    [SerializeField] float dashTime = 0.5f;
    private Vector3 dashDirection;
    private bool isDashing;
    private bool canDash = true;


    [SerializeField] GameObject slamWavePrefab;
    [SerializeField] Transform slamWaveTransform;

    [SerializeField] bool canSlam = false;
    public bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    PlayerInputs playerInputs;
    private Vector3 input;

    
    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
        
    }
    void Update()
    {

        IsGrounded();
        FallGravity();
        
        

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

        
        if (input != Vector3.zero)
        {
            
            rb.linearVelocity = new Vector3 (input.ToIso().x * moveSpeed, rb.linearVelocity.y, input.ToIso().z * moveSpeed);
            
            if (isDashing)
            {
                Debug.Log("dashing while moving");
                rb.linearVelocity += dashDirection * dashVelocity ;
                return;
            }
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashDirection = transform.forward * input.magnitude;
            
            if(dashDirection == Vector3.zero)
            {
                dashDirection = transform.forward * 1.2f; 
            }
            StartCoroutine(StopDashingRoutine());
        }
        

    }

    public void Jump(InputAction.CallbackContext context)
    {   
        
        //Jump
        if ( isGrounded && context.performed )
        {
            rb.linearVelocity = Vector3.up * jumpForce * jumpAcceleration;
            canSlam = true;

            
        }
        //Slam down if in the air

        else if( canSlam && context.performed)
        {
            rb.linearVelocity = -Vector3.up * slamForce;
            canSlam = false;
            //SlamWave();
            StartCoroutine(SlamWaveRoutine());
            
        }
        
        
        
        //Variable jump height
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

    void FallGravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y - fallGravity, rb.linearVelocity.z);
        }
    }

    

    IEnumerator SlamWaveRoutine()
    {
        yield return new WaitUntil(() => isGrounded == true);

        Instantiate(slamWavePrefab, slamWaveTransform.position, Quaternion.identity);
        Debug.Log("wave!");

        
        
        
    }

    IEnumerator StopDashingRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        isDashing = false;
        canDash = true;
    }

}
