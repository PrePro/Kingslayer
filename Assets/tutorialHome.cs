using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialHome : MonoBehaviour {

    public bool tutorialOver = false;
    public GameObject tutorialImg;
	// Use this for initialization
	void Start () {
        Debug.Log("it starts.");
        tutorialImg.SetActive(false);

    }

    void OnTriggerEnter()
    {
        Debug.Log("in this shit.");
        if (tutorialOver == false)
        {
            tutorialImg.SetActive(true);
            Debug.Log("tutorialOver = false");
            Time.timeScale = 0;
            Debug.Log("zero yo");
            tutorialOver = true;
        }
    }
 
	// Update is called once per frame
	void Update () {
		if(tutorialOver == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                tutorialImg.SetActive(false);
                if (Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }
            }
        }
	}
}
