using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneticsManager : MonoBehaviour {

    // Use this for initialization
    BirdController[] birds;
    private Vector3 initialBirdLocation;
    private Vector3 initialCameraLocation;
    public GameObject pipePrefab;
    public bool allBirdsDead = true;
    public GameObject birdPrefab;
	void Start ()
    {
        birds = GetComponentsInChildren<BirdController>();
        initialBirdLocation = birds[0].gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

        allBirdsDead = true;
        for (int i = 0; i < birds.Length; i++)
        {
            if (!birds[i].dead)
                allBirdsDead = false;
            //Debug.Log("Bird " + i + ": " + birds[i].brain.fitness);
        }

        if (allBirdsDead)
        {
            birds = birds.ToList().OrderBy(o => o.brain.fitness).ToArray();
            for (int i = 0; i < 10; i++)
            {
                //Debug.Log(birds.Length);
                BirdController bird1 = birds[chooseBird()];
                BirdController bird2 = birds[chooseBird()];
                /*BirdController bird1 = birds[Random.Range(0, 3)];
                BirdController bird2 = birds[Random.Range(0, 3)];*/
                var child = Instantiate(birdPrefab, initialBirdLocation, Quaternion.identity);
                BirdController childScript = child.GetComponent<BirdController>();
                childScript.brain = NeuralNetwork.CrossOver(bird1.brain, bird2.brain);
                if (Random.Range(0.0f,1.0f) <= 0.1)
                    childScript.brain.Mutate();
                childScript.gameObject.transform.parent = this.gameObject.transform;
             
            }


            for(int i = 0; i < birds.Length;i++)
            {
                DestroyImmediate(birds[i].gameObject);
            }
            birds = GetComponentsInChildren<BirdController>();
            //Debug.Log(birds.Length);



        }

        

	}
    int chooseBird()
    {
        double choice = Random.Range(0, 1);
        if (choice <= 0.4)
            return 0;
        else if (choice <= 0.7)
            return 1;
        else if (choice <= 0.9)
            return 2;
        else
            return 3;
    }

}
