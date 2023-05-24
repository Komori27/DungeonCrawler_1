using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;              // Character's transform
    public float rotationSpeed = 5f;      // Camera rotation speed

    public float lockOnRadius = 10f;      // Maximum lock-on radius
    public List<Transform> enemyTargets;  // List of enemy transforms

    private Transform currentLockOnTarget;  // Currently locked-on enemy
    private int currentIndex = 0;           // Index of the current lock-on target

    private float mouseX;                 // Mouse X input

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;    // Lock the cursor to the game window
        Cursor.visible = false;                      // Hide the cursor

        // Initialize the current lock-on target
        if (enemyTargets.Count > 0)
            currentLockOnTarget = enemyTargets[0];
    }

    private void Update()
    {
        // Get the mouse X input
        mouseX = Input.GetAxis("Mouse X");

        // Rotate the camera around the character
        transform.RotateAround(target.position, Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

        // Check if there are lock-on targets within the radius
        if (enemyTargets.Count > 0)
        {
            // Find the closest lock-on target within the radius
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            foreach (Transform enemyTransform in enemyTargets)
            {
                float distance = Vector3.Distance(target.position, enemyTransform.position);

                if (distance <= lockOnRadius && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = enemyTransform;
                }
            }

            // Set the closest target as the current lock-on target
            currentLockOnTarget = closestTarget;
        }

        // Lock the camera onto the current target if available
        if (currentLockOnTarget != null)
        {
            // Calculate the desired position and rotation for the camera
            Vector3 targetPosition = currentLockOnTarget.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            // Update the camera's position and rotation
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
    }
}
