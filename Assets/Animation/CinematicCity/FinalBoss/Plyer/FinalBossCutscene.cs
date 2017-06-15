using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCutscene : MonoBehaviour {
    //public GameObject player1;
    public QuickCutsceneController qcc;
    public GameObject player2;
    //public Camera cam1;
	// Use this for initialization
	void Start ()
    {
        player2.SetActive(false);
        qcc.ActivateCutscene();
        StartCoroutine("talkingMan");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator talkingMan()
    {
        yield return new WaitForSeconds(7f);
        player2.SetActive(true);
    }
}
