using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfObjects", menuName = "ListOfObjects", order = 2)]
public class AllObjects : ScriptableObject {
    public List<SpawnObject> Objects;
}
