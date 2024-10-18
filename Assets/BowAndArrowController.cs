using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAndArrowController : MonoBehaviour
{
    public GameObject arrowPrefab;   // Reference to the arrow prefab
    public Transform arrowSpawnPoint; // The point from which the arrow will be shot
    public float arrowSpeed = 30.0f;  // Speed at which the arrow will be shot
    public Camera playerCamera;       // Camera reference to align bow with camera
    public float shootDelay = 0.5f;   // Delay between shots

    private bool canShoot = true;

    void Update()
    {
        // Align the bow with the camera's direction
        AlignBowWithCamera();

        // Shoot the arrow when the player presses the left mouse button
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            ShootArrow();
        }
    }

    // Align the bow to match the camera's forward direction
    void AlignBowWithCamera()
    {
        transform.rotation = playerCamera.transform.rotation;
    }

    // Function to handle shooting the arrow
    void ShootArrow()
    {
        // Instantiate the arrow prefab at the spawn point
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        // Add forward force to the arrow (using the bow's forward direction)
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.velocity = arrowSpawnPoint.forward * arrowSpeed;

        // Add a delay between shots
        StartCoroutine(ShootDelay());
    }

    // Coroutine to add delay between shots
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
