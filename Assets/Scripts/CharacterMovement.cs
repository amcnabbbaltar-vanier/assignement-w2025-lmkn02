using UnityEngine;
using Cinemachine;
using System.Collections; // Needed for IEnumerator & Coroutines

[RequireComponent(typeof(Rigidbody))] // Ensures that a Rigidbody component is attached to the GameObject
public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    public bool canDoubleJump = false;  // set true by pickup
    private bool hasUsedDoubleJump = false;  // track if second jump was used

    // ============================== Movement Settings ==============================
    [Header("Movement Settings")]
    [SerializeField] private float baseWalkSpeed = 5f;    // Base speed when walking
    [SerializeField] private float baseRunSpeed = 8f;     // Base speed when running
    [SerializeField] private float rotationSpeed = 10f;   // Speed at which the character rotates

    // ============================== Jump Settings =================================
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;        // Jump force applied to the character
    [SerializeField] private float groundCheckDistance = 1.1f; // Distance to check for ground contact (Raycast)

    // ============================== Modifiable from other scripts ==================
    public float speedMultiplier = 1.0f; // Additional multiplier for character speed ( WINK WINK )

    // ============================== Private Variables ==============================
    private Rigidbody rb; // Reference to the Rigidbody component
    private Transform cameraTransform; // Reference to the camera's transform

    // Input variables
    private float moveX;  // Horizontal input
    private float moveZ;  // Vertical input
    private bool jumpRequest;
    private Vector3 moveDirection; // Movement direction

    // ============================== Animation Variables ==============================
    [Header("Anim values")]
    public float groundSpeed; // Speed value used for animations

    // ============================== Character State Properties ==============================
    /// <summary>
    /// Checks if the character is currently grounded using a Raycast.
    /// If false, the character is in the air.
    /// </summary>
    public bool IsGrounded =>
        Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance);

    /// <summary>
    /// Checks if the player is currently holding the "Run" button.
    /// </summary>
    private bool IsRunning => Input.GetButton("Run");

    // ============================== Unity Built-in Methods ==============================
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        InitializeComponents();
    }

    private void Update()
    {
        RegisterInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // ============================== Initialization ==============================
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Assign the main camera if available
        if (Camera.main)
            cameraTransform = Camera.main.transform;

        // Lock and hide the cursor for better gameplay control
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // ============================== Input Handling ==============================
    private void RegisterInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
    }

    // ============================== Movement Handling ==============================
    private void HandleMovement()
    {
        CalculateMoveDirection();
        HandleJump();
        RotateCharacter();
        MoveCharacter();
    }

    private void CalculateMoveDirection()
    {
        if (!cameraTransform)
        {
            // No camera? Use world space
            moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        }
        else
        {
            // Camera-based forward/right
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * moveZ + right * moveX).normalized;
        }
    }

    private void HandleJump()
    {
        if (jumpRequest)
        {
            
            if (IsGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasUsedDoubleJump = false;
            }
          
            else if (!IsGrounded && canDoubleJump && !hasUsedDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasUsedDoubleJump = true;
                DoFlipAnimation();
            }

            jumpRequest = false;
        }
    }

    
    private void DoFlipAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("FlipTrigger");
        }
    }

    public void RotateCharacter()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void MoveCharacter()
    {
        float speed = IsRunning ? baseRunSpeed : baseWalkSpeed;
        groundSpeed = (moveDirection != Vector3.zero) ? speed : 0.0f;

        Vector3 newVelocity = new Vector3(
            moveDirection.x * speed * speedMultiplier,
            rb.linearVelocity.y,
            moveDirection.z * speed * speedMultiplier
        );

        rb.linearVelocity = newVelocity;
    }

    // ============================== SPEED BOOST LOGIC ==============================
   
    public void StartSpeedBoost(float multiplierValue, float duration)
    {
       
        StopAllCoroutines(); 

        StartCoroutine(SpeedBoostCoroutine(multiplierValue, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplierValue, float boostSeconds)
    {
        float originalMultiplier = speedMultiplier;
        Debug.Log("BOOST START: from " + originalMultiplier + " to " + multiplierValue);

        speedMultiplier = multiplierValue;
        yield return new WaitForSeconds(boostSeconds);

        speedMultiplier = originalMultiplier;
        Debug.Log("BOOST END: Reverted to " + speedMultiplier);
    }
}
