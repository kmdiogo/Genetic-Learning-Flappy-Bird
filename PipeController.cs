using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour {

    Rigidbody2D pipeRigi;
    public float speed = 2;
	// Use this for initialization
	void Start ()
    {
        pipeRigi = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        pipeRigi.velocity = Vector2.left * speed;
        if (transform.position.x <= -61f)
            Destroy(transform.parent.gameObject);
    }

}
