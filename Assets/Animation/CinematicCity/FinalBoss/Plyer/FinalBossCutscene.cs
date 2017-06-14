using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCutscene : MonoBehaviour {
    //public GameObject player1;
    public QuickCutsceneController qcc;
   // public GameObject player2;
    //public Camera cam1;
	// Use this for initialization
	void Start ()
    {

        qcc.ActivateCutscene();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
