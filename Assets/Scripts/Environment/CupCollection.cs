using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCollection : MonoBehaviour
{
    public ParticleSystem CupParticle;
    public static int cupCount = 0;

    void Start ()
    {
	}
	
	void Update ()
    {
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("e"))
            {
                cupCount += 1;
                CupParticle.Play();
                Destroy(gameObject);
            }
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
