using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    public float limit;
    public float distance = 1.5f;
    public float speed;

    private int delete = 0;
    private Vector3 A; // beggining destination
    private Transform Me;

    // Update is called once per frame

    private void Start()
    {
        Me = this.gameObject.GetComponent<Transform>();
        A = this.gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeletePlayer")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
    void Update ()
    {
        Me.position = new Vector3(Me.position.x, Me.position.y, Me.position.z + speed * Time.deltaTime);

        Vector3 B = this.gameObject.transform.position;
        float value = Mathf.Abs((B - A).magnitude);
        if (value >= distance)
        {
            GameObject coin = Instantiate(Resources.Load("Coin")) as GameObject;
            coin.transform.position = new Vector3(B.x - 0.5f, B.y, B.z);
            A = this.gameObject.transform.position;
        }

        if (this.gameObject.GetComponent<Player>().jumpAble == false && delete < 1 && this.gameObject.transform.position.y > limit)
            delete++;
        if (this.gameObject.transform.position.y < limit && delete == 1) // pridať možnosť, že ak je hráč v skoku spawner sa nevymaže skôr...
            Destroy(this.gameObject);
	}


}
