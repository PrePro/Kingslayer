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

    void OnTriggerEnter(Collision col)
    {
        CollisionSpark.Play();
    }
}
