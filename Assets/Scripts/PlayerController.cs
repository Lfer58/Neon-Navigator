using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Movement")]

    [Tooltip("Walk Speed of player")]
    public float walkSpeed;
    public float additionalGravity;
    //Private var - input of movement
    private float moveInput;
    private Vector2 movementInput;

    [Tooltip("How high player jumps")]
    public float jumpForce;
    private bool jumpInput;

    [Tooltip("Amount of jumps player can do without hitting ground")]
    public int jumpAmount;

    [Tooltip("Player Height")]
    public float playerHeight;

    // Variables to check if the player can jump
    [Tooltip("Look for the ground layer")]
    public LayerMask whatIsGround;
    //Player can jump
    private bool readyToJump = true;
    private bool grounded = true;

    [Tooltip("Dash Force")]
    public float dashForce;
    private bool dashPress;

    // Tracker variables
    private float currentSpeed;
    private int currentJumpAmount;

    //Player direction
    private bool faceRight = true;

    // Rigid body for movement
    private Rigidbody rb;
    private PlayerControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        // Intializes the rigid body
        rb = GetComponent<Rigidbody>();
        

        currentJumpAmount = jumpAmount;
    }

    private void OnEnable() {
        if(playerControls == null){
            playerControls = new PlayerControls();

            playerControls.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        MovementInput();

        // Direction
        FlipCharacter();

        // Jumping
        JumpInput();

        // Dashing
        // DashInput();
    }

    // Called at a consistent pace, 50 FPS
    void FixedUpdate()
    {
        // If jump button is pressed, then player jumps
        if (jumpInput)
        {
            Jump();
        }

        // Checks if player is touching the ground
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);
        RaycastHit hit;
        Vector3 rayCastorigin = transform.position;
        if(Physics.SphereCast(rayCastorigin, 0.2f, Vector3.down, out hit, playerHeight * 0.5f, whatIsGround)){
            grounded = true;
        } else {
            grounded = false;
        }

        //Velocity check added to make sure jump isnt reset an extra time
        if (grounded && rb.velocity.y < 0)
        {
            ResetJump();
        }

        if (rb.velocity.y < 0 || !grounded) {
            rb.velocity -= Vector3.up * Time.deltaTime * additionalGravity;
        }

        // if (dashPress)
        // {
        //     Dash();
        // }
    }

    void MovementInput()
    {
        
        moveInput = movementInput.x;
        rb.velocity = new Vector3(moveInput * walkSpeed, rb.velocity.y, 0);
    }

    void FlipCharacter()
    {
        // Change direction of player
        if (!faceRight && moveInput > 0)
        {
            Flip();
        }
        else if (faceRight && moveInput < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceRight = !faceRight;

        if (!faceRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (faceRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
    }

    public bool GetFaceRight() {
        return faceRight;
    }

    void JumpInput()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && readyToJump && currentJumpAmount > 0)
        // Change WasPressedThisFrame() to IsPressed() if you want to be able to hold down jump and repeativily jump
        if (playerControls.Player.Jump.WasPressedThisFrame() && readyToJump && currentJumpAmount > 0)
        {
            jumpInput = true;
        }
    }

    void Jump() 
    {
        jumpInput = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Force that adds upward movement to player
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        currentJumpAmount--;
    }

    void ResetJump ()
    {
        // Debug.Log("Jump Reset");

        readyToJump = true;
        currentJumpAmount = jumpAmount;
    }

    // void DashInput()
    // {
    //     if (Input.GetKeyDown(KeyCode.LeftShift))
    //     {
    //         dashPress = true;
    //     }
    // }

    // void Dash()
    // {
    //     dashPress = false;

    //     rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);

    //     rb.AddForce(transform.right * moveInput * dashForce, ForceMode.Impulse);
    // }
}
