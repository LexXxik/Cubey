using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;

    private Vector3 distance;

	void Start ()
    {
        distance = transform.position - target.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = target.transform.position + distance;
    }
}
