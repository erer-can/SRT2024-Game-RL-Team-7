using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefab;  // Reference to the prefab to be spawned
    private Vector3 spawnLocation = new Vector3(12,12,12);  // Where the prefab will be spawned
    public KeyCode spawnKey = KeyCode.Space;  // Key to press for spawning

    void Update()
    {
        // Check if the spawn key is pressed
        if (Input.GetKeyDown(spawnKey))
        {
            // Spawn the prefab at the specified location and rotation
            Instantiate(prefab, spawnLocation, Quaternion.identity);
        }
    }
}

