using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstFight : MonoBehaviour {
    private GameObject firstEnemy;
    //public Canvas tutorialFight;
	// Use this for initialization
	void Start () {
        firstEnemy = GameObject.FindGameObjectWithTag("FirstEnemy");
	}
	
	// Update is called once per frame
	void Update () {
        if(firstEnemy == null)
        {
            Destroy(gameObject);
        }
		
	}

    /*void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            tutorialFight.gameObject.SetActive(true);
        }
    }*/

}
