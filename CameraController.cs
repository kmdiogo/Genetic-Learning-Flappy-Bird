using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Rigidbody2D CameraRigi;
    public float speed = 2;
	// Use this for initialization
	void Start ()
    {
        CameraRigi = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
