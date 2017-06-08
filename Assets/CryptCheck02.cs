using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptCheck02 : MonoBehaviour {
    private GameObject mainKnight;
    private GameObject mainKnight2;
    public GameObject gate;
    public GameObject gate2;
    private bool manDown;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mainKnight = GameObject.FindGameObjectWithTag("CryptMainKnight2");
        if (mainKnight == null)
        {
            gate.SetActive(false);
        }
        else
        {
            //
        }
        mainKnight2 = GameObject.FindGameObjectWithTag("CryptMainKnight3");

        if (mainKnight2 == null)
        {
            gate2.SetActive(false);
        }
        else
        {
            //
        }

    }
}
