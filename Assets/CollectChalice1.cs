using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectChalice1 : MonoBehaviour
{
    public Renderer rend;

	void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
	
	void Update ()
    {
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && CupCollection.cupCount >= 1)
        {
            PuzzleWall.cupsDelivered += 1;
            rend.enabled = true;
        }
    }
}
