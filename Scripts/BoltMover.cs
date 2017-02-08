using UnityEngine;
using System.Collections;

public class BoltMover : MonoBehaviour {


    public float speed;
    public float lifetime;
    public int damage;

	// Use this for initialization
	void Start () {
   
        GetComponent<Rigidbody>().velocity = transform.up * speed;
        Destroy(gameObject.transform.parent.gameObject, lifetime );
    }


}
