using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public bool Get = false;

    private Transform myTransform;
    static private MapGenerator manager;
    public float rotationSpeed;
    public float peak;
    public float bottom;
    public float hoverSpeed;

    private float target;
    static private Player player;

    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        manager = GameObject.Find("_GameSetting").GetComponent<MapGenerator>();
        myTransform = this.gameObject.GetComponent<Transform>();
        peak = myTransform.position.y + peak;
        bottom = myTransform.position.y - bottom;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetCoin();
            gameObject.transform.Find("CoinSplash").gameObject.SetActive(true);
            gameObject.transform.Find("Border").gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.Find("Core").gameObject.GetComponent<MeshRenderer>().enabled = false;
            Get = true;
        }
    }

    void LateUpdate ()
    {
        if (player.transform.position.z + 20f >= myTransform.position.z && Get == false)
        {
            if (myTransform.position.y >= peak || myTransform.position.y <= bottom)
                hoverSpeed = -1 * hoverSpeed;
            RotateChild(0);
            RotateChild(1);
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + hoverSpeed * Time.deltaTime, myTransform.position.z);
        }
    }

    void RotateChild(int index)
    {
        Transform Child = this.gameObject.transform.GetChild(index);
        Child.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
