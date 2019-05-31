using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour {


    public GameObject prefab;
    GeneticsManager gm;
	// Use this for initialization
	void Start ()
    {
        gm = GameObject.Find("GeneticsManager").GetComponent<GeneticsManager>();
        InvokeRepeating("CreatePipe", 0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gm.allBirdsDead)
        {
            CancelInvoke();
            InvokeRepeating("CreatePipe", 0.0f, 1.0f);
        }
            
    }

    void CreatePipe()
    {
        
       var newPipe = Instantiate(prefab, new Vector3(-38, 4.91f, 0),Quaternion.identity);
    }
}
