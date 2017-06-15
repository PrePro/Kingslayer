using System.Collections;
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
    //public GameObject burn;
    //public GameObject start;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject bossHealthBar;
    public GameObject bossName;
    public GameObject wiz;
    public GameObject wiz2;
    public GameObject gameUI;
    public Movement move;
    public ParticleSystem psSmoke;
    public GameObject child1;
    public GameObject child2;
    public GameObject cauldron;
	// Use this for initialization
	void Start () {
        respawnPoint = GameObject.FindGameObjectWithTag("StartPoint");
        camera1.SetActive(false);
        camera2.SetActive(false);
        camera3.SetActive(false);
        bossName.SetActive(false);
        bossHealthBar.SetActive(false);
        wiz.SetActive(false);
        wiz2.SetActive(false);
        gameUI.SetActive(false);
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
            move.stopMovement = true;
            respawnPoint.transform.position = newSpawn.transform.position;
            BossFightMusic.PlayDelayed(0.1f);
            Music.SetActive(false);
            Music2.SetActive(false);
            if (itsRunning == false)
            {
                StartCoroutine("bossCine");
            }
        }
    }

    IEnumerator bossCine()
    {
        itsRunning = true;
        wiz2.SetActive(true);
        camera1.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("bossCine2");
        //burn.SetActive(true);
        //start.SetActive(true);
   
    }

    IEnumerator bossCine2()
    {
        camera2.SetActive(true);
        camera1.SetActive(false);
        yield return new WaitForSeconds(4.5f);
        //wiz2.GetComponent<Animator>().SetInteger("BossStuff", 1);
        StartCoroutine("bossCine3");
    }

    IEnumerator bossCine3()
    {
        camera2.SetActive(false);
        camera3.SetActive(true);
        psSmoke.Play();
        StartCoroutine("childParticle");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("finalPart");
       
    }
    IEnumerator childParticle()
    {
        yield return new WaitForSeconds(1.5f);
        child1.SetActive(false);
        child2.SetActive(false);
        psSmoke.Stop();
    }

    IEnumerator finalPart()
    {
        yield return new WaitForSeconds(2.5f);
        camera3.SetActive(false);
        bossName.SetActive(true);
        bossHealthBar.SetActive(true);
        wiz2.SetActive(false);
        wiz.SetActive(true);
        gameUI.SetActive(true);
        cauldron.SetActive(false);
        move.stopMovement = false;
        itsDone = true;
    }
}
