using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Rotation speed

    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] AttackController attackController;
    [SerializeField] LockOnManager lockOnManager;

    private void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            // Get the direction of movement from the velocity vector
            Vector3 movementDirection = rb.velocity.normalized;

            // Calculate the target rotation based on the movement direction
            Quaternion targetRotation;

            if (playerController.isStrafing)
            {
                // Calculate the rotation to face away from the camera when strafing
                Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
                targetRotation = Quaternion.LookRotation(cameraForward);
            }
            else
            {
                // Calculate the rotation to face the movement direction
                targetRotation = Quaternion.LookRotation(movementDirection);
            }

            // Smoothly rotate the character towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else if (lockOnManager.isLockedOn && !attackController.attackReady)
        {
            // Calculate the target rotation based on the movement direction
            Quaternion targetRotation;
            // Calculate the target rotation to face the lock-on target
            Vector3 targetDirection = lockOnManager.currentLockOnTarget.transform.position - transform.position;
            targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // Smoothly rotate the character towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
