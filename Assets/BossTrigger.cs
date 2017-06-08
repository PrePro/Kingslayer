using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    private GameObject respawnPoint;
    public GameObject newSpawn;
    private bool itsDone = false;
    public AudioSource BossFightMusic;
    public GameObject Music;
    WizardBoss wiz;
	// Use this for initialization
	void Start () {
        respawnPoint = GameObject.FindGameObjectWithTag("StartPoint");		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Up in here");
        if(other.tag == "Player" && itsDone == false)
        {
            Music.SetActive(false);
            BossFightMusic.PlayDelayed(0.1f);
            wiz.turnOnWizard = true;
            Debug.Log("Down in here");
            respawnPoint.transform.position = newSpawn.transform.position;
            itsDone = true;

        }
    }
}
