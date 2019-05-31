using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    Transform half1;
    Transform half2;
    Transform start;
    Transform center;
    public float GapSize = 3.75f;
    GeneticsManager gm;
	// Use this for initialization
	void Start ()
    {
        gm = GameObject.Find("GeneticsManager").GetComponent<GeneticsManager>();
        start = this.transform;
        float RandScale = Random.Range(1f, 14.57f);
        half1 = transform.GetChild(0);
        half2 = transform.GetChild(1);
        center = transform.GetChild(2);
        half2.localScale = new Vector3(1, RandScale,1);
        half1.localScale = new Vector3(1, 15.57f - RandScale, 1);
        center.position = new Vector3(half2.transform.position.x, half2.position.y - (0.5f * half2.localScale.y) - 2, 0);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gm.allBirdsDead)
        {
            Destroy(gameObject);
        }
    }



}
