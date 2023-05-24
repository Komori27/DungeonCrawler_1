using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;        // Movement speed
    public float rotationSpeed = 100f;  // Rotation speed
    public float sprintMultiplier = 2f; // Speed multiplier when sprinting
    public float walkMultiplier = 0.5f;
    public bool isSprinting = false;

    private float currentSpeed;         // Current movement speed
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;
    public bool isStrafing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Movement input
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Strafe input
        float strafeInput = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            strafeInput = -1f; // Strafe left
            animator.SetBool("Strafing", true);
            animator.SetFloat("StrafeDir", -1);
            isStrafing = true;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            strafeInput = 1f; // Strafe right
            animator.SetBool("Strafing", true);
            animator.SetFloat("StrafeDir", 1);
            isStrafing = true;
        }
        else 
        {
            animator.SetBool("Strafing", false);
            animator.SetFloat("StrafeDir", 0);
            isStrafing = false;
        }


        // Calculate the movement direction relative to the camera's forward and right directions
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = cameraTransform.right;

        // Calculate the strafe direction relative to the camera's right direction
        Vector3 strafeDirection = strafeInput * cameraRight;

        // Calculate the movement vector
        Vector3 movement = (moveVertical * cameraForward + moveHorizontal * cameraRight + strafeDirection).normalized;

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed * sprintMultiplier;
            isSprinting = true;
        }
        else
        {
            currentSpeed = moveSpeed;
            isSprinting = false;
        }
        if(isStrafing)
        {
            currentSpeed = moveSpeed * walkMultiplier;
            isSprinting = false;
        }

        // Apply movement
        rb.velocity = movement * currentSpeed;

        // Set the "Speed" parameter in the animator
        float speed = rb.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
