using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "SpawnObject", order = 1)]
public class SpawnObject: ScriptableObject{
    public GameObject prefab;
    public int spawnRate;
    public float availableSpawn;
    public Vector3 space;
}
