using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manAtArmsTutorial : MonoBehaviour {

    public GameObject manAtArmsTut;
    private bool tutorialDone;
    private bool doneOnce = false;
	// Use this for initialization
	void Start ()
    {
        manAtArmsTut.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(tutorialDone == true)
        {
            Debug.Log("Use R or LeftTrigger bitch");
            if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("LeftTrigger") == 1)
            {
                manAtArmsTut.SetActive(false);
                if(Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && doneOnce == false)
        {
            Time.timeScale = 0;
            manAtArmsTut.SetActive(true);
            tutorialDone = true;
            doneOnce = true;
            
        }
    }
}
