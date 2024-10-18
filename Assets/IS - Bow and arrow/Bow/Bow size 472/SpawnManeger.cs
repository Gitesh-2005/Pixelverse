using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] arrows; // Array of arrow prefabs
    private float startDelay = 1.0f; // Delay before starting to spawn
    private float spawnInterval = 3.0f; // Time between spawns

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnArrow", startDelay, spawnInterval); // Start spawning arrows
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnArrow()
{
    if (arrows == null || arrows.Length == 0)
    {
        Debug.LogError("Arrow array is not initialized or empty!");
        return; // Exit the method to avoid accessing an empty array
    }

    // Determine a random spawn position (e.g., within a specific range)
    float spawnX = Random.Range(-5.0f, 5.0f); // Change these values as needed
    float spawnY = 1.0f; // Height of spawn position (adjust as needed)
    float spawnZ = 10.0f; // Z position in front of the camera (adjust as needed)

    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ); // Create the spawn position

    // Choose a random arrow from the array
    int arrowIndex = Random.Range(0, arrows.Length);

    // Instantiate the arrow at the spawn position with a default rotation
    Instantiate(arrows[arrowIndex], spawnPos, Quaternion.identity);
}

}
