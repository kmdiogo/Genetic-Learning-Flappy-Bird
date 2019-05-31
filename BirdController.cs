using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    Rigidbody2D BirdRigi;
    public LayerMask collisionMask;
    public float speed = 2;
    public float jumpForce = 300;
    public NeuralNetwork brain;
    public float distanceTravelled;
    public bool dead = false;
    float vertDistance;
    float horizontalDistance;

    RaycastHit2D hit;
    GameObject closestPipeCenter;
    float MINHORDISTANCE = 0f;
    float MAXHORDISTANCE = 10f;
    float MINVERTDISTANCE = 0f;
    float MAXVERTDISTANCE = 15f;
	// Use this for initialization
	void Start ()
    {
        BirdRigi = GetComponent<Rigidbody2D>();
        brain = new NeuralNetwork(2, 10, 1);
        distanceTravelled = 0f;
        closestPipeCenter = gameObject;
        hit = Physics2D.Raycast(transform.position, Vector2.right, collisionMask);
    }
	
	// Update is called once per frame
	void Update ()
    {
        distanceTravelled += 1;
        /*hit = Physics2D.Raycast(transform.position, Vector2.right, collisionMask);
        if (hit)
        {
            horizontalDistance = (Mathf.Abs(hit.collider.transform.position.x - transform.position.x));
            vertDistance = (Mathf.Abs(hit.collider.transform.position.y - transform.position.y));
            //Debug.Log(horizontalDistance);
            //Debug.Log(hit.collider.transform.parent.name);
        }*/
        closestPipeCenter = GetClosestPipeCenter();
        horizontalDistance = (Mathf.Abs(closestPipeCenter.transform.position.x - transform.position.x));
        vertDistance = ((closestPipeCenter.transform.position.y - transform.position.y));

        float brainDecision = brain.Query(new float[] { horizontalDistance, vertDistance })[0];
        if (brainDecision > 0.5f)
        {
            BirdRigi.velocity = Vector2.up * jumpForce;
            //Debug.Log(brainDecision);
        }


        //Player control
        /*if (Input.GetKeyDown(KeyCode.Space))
            BirdRigi.velocity = Vector2.up * jumpForce;*/
	}

    float ScaleHorizontalInput(float x)
    {
        float percent = (x - MINHORDISTANCE) / (MAXHORDISTANCE - MINHORDISTANCE);
        return percent * (0.99f - 0.01f) + 0.01f;
    }

    float ScaleVerticalInput(float x)
    {
        float percent = (x - MINVERTDISTANCE) / (MAXVERTDISTANCE - MINVERTDISTANCE);
        return percent * (0.99f - 0.01f) + 0.01f;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            brain.fitness = distanceTravelled - (closestPipeCenter.transform.position.x - transform.position.x);
            //Debug.Log(brain.fitness);
            this.gameObject.SetActive(false);
            dead = true;
        }
    }

    GameObject GetClosestPipeCenter()
    {
        var centers = GameObject.FindGameObjectsWithTag("PipeCenter");
        GameObject lowest;
        lowest = this.gameObject;
        foreach (GameObject center in centers)
        {
            if (center.transform.position.x - transform.position.x > 0)
                lowest = center;
        }

        foreach (GameObject center in centers)
        {
            var dist = center.transform.position.x - transform.position.x;
            if (dist > 0 && dist < lowest.transform.position.x - transform.position.x)
                lowest = center;
        }

        //Debug.Log(lowest.transform.position.x - transform.position.x);
        return lowest;
    }

}
