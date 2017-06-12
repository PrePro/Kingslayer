﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    private GameObject respawnPoint;
    public GameObject newSpawn;
    private bool itsDone = false;
    private bool itsRunning = false;
    public AudioSource BossFightMusic;
    public GameObject Music;
    public GameObject Music2;
    public GameObject burn;
    public GameObject start;
    public GameObject camera1;
    public GameObject bossHealthBar;
    public GameObject bossName;
    //WizardBoss wiz;
	// Use this for initialization
	void Start () {
        respawnPoint = GameObject.FindGameObjectWithTag("StartPoint");
        camera1.SetActive(false);
        bossName.SetActive(false);
        bossHealthBar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(camera1.activeSelf == true && itsDone == true)
        {
            camera1.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && itsDone == false)
        {
            BossFightMusic.PlayDelayed(0.1f);
            if (itsRunning == false)
            {
                StartCoroutine("bossCine");
            }
        }
    }

    IEnumerator bossCine()
    {
        itsRunning = true;
        camera1.SetActive(true);
        yield return new WaitForSeconds(2f);
        Music.SetActive(false);
        Music2.SetActive(false);
        burn.SetActive(true);
        start.SetActive(true);
        bossName.SetActive(true);
        bossHealthBar.SetActive(true);
        //wiz.turnOnWizard = true;
        respawnPoint.transform.position = newSpawn.transform.position;
        itsDone = true;
    }
}
