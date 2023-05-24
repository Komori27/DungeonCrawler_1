using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;                    // Reference to the Animator component
    public float walkAnimationSpeed = 1f;        // Animation speed for walking
    public float sprintAnimationSpeed = 1.5f;    // Animation speed for sprinting

    private PlayerController playerController;   // Reference to the PlayerController component

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Set the animation speed multiplier based on the character's sprinting state
        if (playerController.isSprinting)
        {
            animator.SetFloat("SpeedMultiplier", sprintAnimationSpeed);
        }
        else
        {
            animator.SetFloat("SpeedMultiplier", walkAnimationSpeed);
        }
    }
}
