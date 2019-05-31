using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour {

    Rigidbody2D rigi;
	// Use this for initialization
	void Start ()
    {
        rigi = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		rigi.velocity = Vector2.left * 5;
    }
}
