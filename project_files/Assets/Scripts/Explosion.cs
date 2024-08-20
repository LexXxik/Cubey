using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public string boxName;

    public int numberOfBoxes;
    public float sizeRange;
    public float rangeFromCenter;
    public Vector3 velocityRange;

	void Start ()
    {
        for (int i = 0; i < numberOfBoxes; i++)
        {
            GameObject box = Instantiate(Resources.Load(boxName)) as GameObject;
            float scale = Random.Range(0.1f, sizeRange);
            box.transform.localScale = new Vector3(scale,scale,scale);
            float position = Random.Range(0.05f, rangeFromCenter);
            box.transform.position = new Vector3(this.gameObject.transform.position.x + position, this.gameObject.transform.position.y + position, this.gameObject.transform.position.z + position);
            box.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(0,velocityRange.x), Random.Range(0, velocityRange.y), Random.Range(0, velocityRange.z));
        }
	}
	
}
