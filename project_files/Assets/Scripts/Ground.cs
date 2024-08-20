using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground: MonoBehaviour {

    private Transform parent;
    private MapGenerator mapGenerator;

    private static Vector3 nextGroundPosition = new Vector3(2.5f, -0.5f, 55);

    private void Start()
    {
        parent = this.gameObject.transform.parent.gameObject.transform;
        mapGenerator = GameObject.Find("_GameSetting").GetComponent<MapGenerator>();
        nextGroundPosition.z = 55f;
    }

    private void OnTriggerEnter(Collider other) // fixed!
    {
        if (other.tag == "Player")
            MoveGround();
            
    }


    private void MoveGround()
    {
        parent.position = nextGroundPosition;
        nextGroundPosition = new Vector3(2.5f, -0.5f, parent.position.z + 10f);
        mapGenerator.CreatingSegment();
    }
}
