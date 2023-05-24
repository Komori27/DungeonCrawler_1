using System.Collections.Generic;
using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    public float lockOnRadius = 10f;                  // Maximum lock-on radius
    public List<LockOnTarget> lockOnTargets;          // List of LockOnTarget scripts
    public GameObject cameraObject;                   // Camera GameObject to rotate when locked on
    public GameObject cameraOrigin;

    public KeyCode resetCameraKey = KeyCode.X;         // Key to reset the camera

    public LockOnTarget currentLockOnTarget;         // Currently locked-on target
    private int currentIndex = 0;                     // Index of the current lock-on target

    public bool isLockedOn = false;                   // Flag to indicate if the camera is locked on

    public CameraController cameraController;        // Reference to the CameraController script

    private void Start()
    {
        // Find all LockOnTarget scripts in the scene and add them to the lockOnTargets list
        LockOnTarget[] targets = FindObjectsOfType<LockOnTarget>();
        lockOnTargets.AddRange(targets);

        // Set the current lock-on target to the first one in the list
        if (lockOnTargets.Count > 0)
        {
            currentLockOnTarget = lockOnTargets[0];
        }

    }

    private void Update()
    {
        // Detect left input
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchLockOnTarget(-1);
        }

        // Detect right input
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchLockOnTarget(1);
        }

        // Detect reset camera input
        if (Input.GetKeyDown(KeyCode.X))
        {
            ResetCamera();
        }
    }

    private void SwitchLockOnTarget(int direction)
    {
        // Calculate the next index
        currentIndex += direction;
        if (currentIndex < 0)
            currentIndex = lockOnTargets.Count - 1;
        else if (currentIndex >= lockOnTargets.Count)
            currentIndex = 0;

        // Set the current lock-on target to the new LockOnTarget script
        currentLockOnTarget = lockOnTargets[currentIndex];

        // Lock the camera onto the new target
        LockCameraOnTarget(currentLockOnTarget);
    }

    private void LockCameraOnTarget(LockOnTarget target)
    {
        // Check if the target reference is valid
        if (target != null)
        {
            // Calculate the desired rotation for the camera
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - cameraObject.transform.position);

            // Apply only the rotation component of the target rotation to the camera's rotation
            cameraObject.transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

            // Set the locked-on flag to true
            isLockedOn = true;
        }
    }


    private void ResetCamera()
    {
        // Reset the camera's rotation to the original rotation
        cameraObject.transform.rotation = cameraOrigin.transform.rotation;
        cameraObject.transform.position = cameraOrigin.transform.position;
        // Set the locked-on flag to false
        isLockedOn = false;
    }

    private void LateUpdate()
    {
        // Check if the camera is locked on
        if (isLockedOn)
        {
            // Constantly lock the camera onto the current target
            LockCameraOnTarget(currentLockOnTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere to visualize the lock-on radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lockOnRadius);
    }
}
