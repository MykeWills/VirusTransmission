using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public float speed;
    public float flySpeed;
    public bool flyAway = false;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetBool("Walk", true);
	}

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flyAway = !flyAway;
            GetComponent<Animator>().SetBool("Fly", flyAway);
        }
        
    }

    void FixedUpdate ()
    {

        if (!flyAway) {
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxis("Horizontal") * -1 * speed, GetComponent<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * -1 * speed);
        }
        else if (flyAway)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxis("Horizontal") * -1 * speed, GetComponent<Rigidbody>().velocity.y + flySpeed, Input.GetAxis("Vertical") * -1 * speed);
        }
    }
}
