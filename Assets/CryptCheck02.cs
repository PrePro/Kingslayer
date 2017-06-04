using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptCheck02 : MonoBehaviour {
    private GameObject mainKnight;
    public GameObject gate;
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

    }
}
