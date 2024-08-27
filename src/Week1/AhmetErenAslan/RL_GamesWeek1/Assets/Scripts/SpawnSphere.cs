using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnSphere : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private Transform spawnPoint;

    private void Update() {

        if (Input.GetKeyUp(KeyCode.E)) {
            Transform prefabTransform = Instantiate(prefab, spawnPoint);
            prefabTransform.parent = spawnPoint;
            prefabTransform.localPosition = Vector3.zero;
        }
        
    }

}
