using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCutscene : MonoBehaviour {
    //public GameObject player1;
    public QuickCutsceneController qcc;
    public GameObject player2;
    private Animator myanim;
    //public Camera cam1;
	// Use this for initialization
	void Start ()
    {
        myanim = player2.GetComponent<Animator>();
        player2.SetActive(false);
        qcc.ActivateCutscene();
        StartCoroutine("talkingMan");
        StartCoroutine("pointingMan");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator talkingMan()
    {
        yield return new WaitForSeconds(7f);
        player2.SetActive(true);
    }

    IEnumerator pointingMan()
    {
        yield return new WaitForSeconds(8.5f);
        myanim.SetInteger("StopPoint", 1);
    }
}
