using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialHome : MonoBehaviour {

    public bool tutorialOver = false;
    public GameObject tutorialImg;
    public GameObject tutorialImg2;
    public GameObject tutorialImg3;
    public GameObject startPos;
    public GameObject respawnPoint;

	// Use this for initialization
	void Start ()
    {
        tutorialImg.SetActive(false);
        tutorialImg2.SetActive(false);
        tutorialImg3.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (tutorialOver == false && other.tag == "Player")
        {
            startPos.transform.position = respawnPoint.transform.position;
            StartCoroutine("startGame");
            //tutorialOver = true;
        }
    }
 
	// Update is called once per frame
	void Update () {
		if(tutorialOver == true)
        {
            Debug.Log("Use R or LeftTrigger bitch");
            if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("LeftTrigger") == 1)
            {
                tutorialImg.SetActive(false);
                tutorialImg2.SetActive(false);
                tutorialImg3.SetActive(false);
                if (Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }
            }
        }
	}

    IEnumerator fadeIn()
    {
        tutorialImg.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine("moralityUI");
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(4f);
        
        StartCoroutine("fadeIn");
    }

    IEnumerator moralityUI()
    {
        tutorialImg2.SetActive(true);
        //tutorialImg.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine("minimapUI");
    }

    IEnumerator minimapUI()
    {
        yield return new WaitForSeconds(1f);
        tutorialImg3.SetActive(true);
        tutorialOver = true;
    }
}
