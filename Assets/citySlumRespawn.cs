using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class citySlumRespawn : MonoBehaviour {
    public GameObject startpos;
    public GameObject respawn;
	// Use this for initialization
	void Start () {
        startpos.transform.position = respawn.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
