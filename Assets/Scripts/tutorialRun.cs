using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialRun : MonoBehaviour {

    public bool tutorialOver = false;
    public GameObject tutorialRunner;
    // Use this for initialization
    void Start()
    {
        tutorialRunner.SetActive(false);
    }

    void OnTriggerEnter()
    {
        if (tutorialOver == false)
        {
            tutorialRunner.SetActive(true);
            Debug.Log("tutorialOver = false");
            Time.timeScale = 0;
            tutorialOver = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialOver == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                tutorialRunner.SetActive(false);
                if (Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                }
            }
        }
    }
}
