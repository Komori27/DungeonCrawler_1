using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectileSpawner;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float launchForce = 15f; // Initial force applied to the projectile
    public float rotationRange = 10f;

    private float launchRotation;

    public GameObject characterRotation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Calculate the launch direction based on the character's facing direction
            Vector3 launchDirection = projectileSpawner.transform.forward;

            // Randomly rotate the spawner on its local Y axis within the specified range
            float randomRotation = Random.Range(-rotationRange, rotationRange);
            projectileSpawner.transform.localRotation = Quaternion.Euler( -24.379f , randomRotation , 0f );

            // Spawn a new projectile
            GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawner.transform.position, projectileSpawner.transform.rotation);

            // Get the Rigidbody component of the projectile
            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

            // Apply force to the projectile in the launch direction
            projectileRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        }
    }

    void CastSpell() 
    {
        // Calculate the launch direction based on the character's facing direction
        Vector3 launchDirection = projectileSpawner.transform.forward;

        // Randomly rotate the spawner on its local Y axis within the specified range
        float randomRotation = Random.Range(-rotationRange, rotationRange);
        projectileSpawner.transform.localRotation = Quaternion.Euler(-24.379f, randomRotation, 0f);

        // Spawn a new projectile
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawner.transform.position, projectileSpawner.transform.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

        // Apply force to the projectile in the launch direction
        projectileRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }
}