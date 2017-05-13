using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionSpark : MonoBehaviour
{
    public ParticleSystem CollisionSpark;

    void Start ()
    {

    }
	
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        CollisionSpark.Play();
    }
}
