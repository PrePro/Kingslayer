using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCollection : MonoBehaviour
{

    //public ParticleSystem CupParticle;
    public static int cupCount = 0;

    void Start ()
    {
	}
	
	void Update ()
    {	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            cupCount += 1;
           // Instantiate(CupParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
