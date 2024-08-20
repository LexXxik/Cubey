using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlate : MonoBehaviour
{

    public float jump;

    private GameObject player;
    private GameObject spawn;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Start()
    {
        spawn = Instantiate(player);
        spawn.tag = "Spawner";
        Player script = spawn.GetComponent<Player>();
        CoinSpawner coinS = spawn.AddComponent<CoinSpawner>();
        coinS.speed = script.speed;
        coinS.limit = this.gameObject.transform.position.y + 0.4f;
        script.enabled = false;
        spawn.GetComponent<MeshRenderer>().enabled = false;
        spawn.transform.position = new Vector3(this.gameObject.transform.position.x + 0.5f, this.transform.position.y + 0.4f, this.transform.position.z - 1f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Spawner")
        {
            Rigidbody RB = other.GetComponent<Rigidbody>();
            Player player = other.GetComponent<Player>();
            player.jumpAble = false;
            RB.AddForce(transform.up * player.jumping * jump);
            if (other.gameObject.tag == "Spawner")
            {
                other.isTrigger = true;
            }
        }
    }
}