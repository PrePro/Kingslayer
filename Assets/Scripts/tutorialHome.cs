using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialHome : MonoBehaviour {

    public bool tutorialOver = false;
    public GameObject tutorialImg;
    public GameObject tutorialImg2;
    public GameObject tutorialImg3;

	// Use this for initialization
	void Start ()
    {
        tutorialImg.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (tutorialOver == false && other.tag == "Player")
        {
            StartCoroutine("startGame");
            tutorialOver = true;
        }
    }
 
	// Update is called once per frame
	void Update () {
		if(tutorialOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("LeftTrigger") == 1)
            {
                tutorialImg.SetActive(false);
                if (Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }
            }
        }
	}

    IEnumerator fadeIn()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(3f);
        tutorialImg.SetActive(true);
        StartCoroutine("fadeIn");
    }

    IEnumerator moralityUI()
    {
        tutorialImg2.SetActive(true);
        tutorialImg.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine("minimapUI");
    }

    IEnumerator minimapUI()
    {
        tutorialImg2.SetActive(false);
        tutorialImg3.SetActive(true);
        yield return new WaitForSeconds(2f);

    }
}
