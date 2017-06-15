using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCutscene : MonoBehaviour {
    //public GameObject player1;
    public QuickCutsceneController qcc;
    public QuickCutsceneController qcc2;
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
        qcc2.ActivateCutscene();
        player2.SetActive(true);

    }

    IEnumerator pointingMan()
    {
        yield return new WaitForSeconds(8.5f);
        myanim.SetInteger("StopPoint", 1);
    }
    IEnumerator wizardTalk1()
    {
        Time.timeScale = .5f;
        yield return new WaitForSeconds(5.5f);
        Time.timeScale = 1;
        //privoTalk();
    }
    IEnumerator privoTalk1()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = .5f;
        princeTalk();
    }

    public void privoTalk()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        StartCoroutine("privoTalk1");
    }
    public void princeTalk()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        StartCoroutine("wizardTalk1");
    }
}
