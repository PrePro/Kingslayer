using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCutscene : MonoBehaviour {
    //public GameObject player1;
    public QuickCutsceneController qcc;
    public QuickCutsceneController qcc2;
    public QuickCutsceneController qcc3;
    public QuickCutsceneController qcc4;
    public QuickCutsceneController qcc5;
    public QuickCutsceneController qcc6;
    public QuickCutsceneController qcc7;
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;
    public GameObject dialogue4;
    public GameObject dialogue5;
    public GameObject playerRun;
    public GameObject player2;
    public GameObject wiz;
    private Animator myanim;
    private bool firstScene = false;
    private bool secondScene = false;
    private bool thirdScene = false;
    private bool fourthScene = false;
    private bool fifthScene = false;
    private bool doOnce = false;
    private bool doOnce1 = false;
    private bool doOnce2 = false;
    private bool check = false;
    public ParticleSystem psExplosion;
    public GameObject endScreen;
    //public Camera cam1;
    // Use this for initialization
    void Start ()
    {
        myanim = player2.GetComponent<Animator>();
        player2.SetActive(false);
        qcc.ActivateCutscene();
        StartCoroutine("startingMan");
        //StartCoroutine("talkingMan");
        StartCoroutine("pointingMan");
        dialogue1.SetActive(false);
        dialogue2.SetActive(false);
        dialogue3.SetActive(false);
        dialogue4.SetActive(false);
        dialogue5.SetActive(false);
        endScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(firstScene == true && doOnce == false && check == false)
        {
            if (Input.GetKey(KeyCode.JoystickButton1))
            {
                secondCutscene();
                check = true;
                doOnce = true;
                
            }
        }
        if (secondScene == true && doOnce1 == false && check == false)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                thirdCutscene();
                check = true;
                doOnce1 = true;
               
            }
        }
        if (thirdScene == true && doOnce2 == false && check == false)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                fourthCutscene();
                check = true;
                doOnce2 = true;
            }
        }
        if(fourthScene == true && check == false)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                fifthCutscene();
                check = true;
             
            }
        }
        if(fifthScene == true && check == false)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                sixthCutscene();
                check = true;
                
            }
        }

    }
    IEnumerator startingMan()
    {
        yield return new WaitForSeconds(6.8f);
        qcc.EndCutscene();
        dialogue1.SetActive(true);
        qcc2.ActivateCutscene();
        StartCoroutine("talkingMan");
    }
    IEnumerator talkingMan()
    {
        playerRun.SetActive(false);
        firstScene = true;
        yield return new WaitForSeconds(1f);
        check = false;
        yield return new WaitForSeconds(6f);
        firstScene = false;
        secondCutscene();
    }
    IEnumerator accusingMan()
    {
        secondScene = true;
        yield return new WaitForSeconds(1f);
        check = false;
        yield return new WaitForSeconds(6f);
        secondScene = false;
        thirdCutscene();
    }
    IEnumerator laughingMan()
    {
        thirdScene = true;
        yield return new WaitForSeconds(1f);
        check = false;
        yield return new WaitForSeconds(6f);
        thirdScene = false;
        fourthCutscene();
    }
    IEnumerator cryingMan()
    {
        fourthScene = true;
        yield return new WaitForSeconds(1f);
        check = false;
        yield return new WaitForSeconds(8f);
        fourthScene = false;
        fifthCutscene();
    }
    IEnumerator tauntingMan()
    {
        fifthScene = true;
        yield return new WaitForSeconds(1f);
        check = false;
        yield return new WaitForSeconds(7f);
        fifthScene = false;
        sixthCutscene();
    }

    public void secondCutscene()
    {
        dialogue1.SetActive(false);
        StopCoroutine("talkingMan");
        qcc2.EndCutscene();
        player2.SetActive(true);
        dialogue2.SetActive(true);
        qcc3.ActivateCutscene();
        StartCoroutine("accusingMan");
    }
    public void thirdCutscene()
    {
        dialogue2.SetActive(false);
        StopCoroutine("accusingMan");
        qcc3.EndCutscene();
        qcc4.ActivateCutscene();
        dialogue3.SetActive(true);
        wiz.GetComponent<Animator>().SetInteger("NextAction", 2);
        StartCoroutine("laughingMan");
    }
    public void fourthCutscene()
    {
        dialogue3.SetActive(false);
        StopCoroutine("laughingMan");
        qcc4.EndCutscene();
        qcc5.ActivateCutscene();
        dialogue4.SetActive(true);
        player2.GetComponent<Animator>().SetInteger("StopPoint", 1);
        StartCoroutine("cryingMan");
    }
    public void fifthCutscene()
    {
        dialogue4.SetActive(false);
        StopCoroutine("cryingMan");
        qcc5.EndCutscene();
        qcc6.ActivateCutscene();
        dialogue5.SetActive(true);
        wiz.GetComponent<Animator>().SetInteger("NextAction", 3);
        StartCoroutine("tauntingMan");
    }
    public void sixthCutscene()
    {
        dialogue5.SetActive(false);
        StopCoroutine("tauntingMan");
        qcc6.EndCutscene();
        wiz.GetComponent<Animator>().SetInteger("NextAction", 4);
        player2.GetComponent<Animator>().SetInteger("StopPoint", 3);
        qcc7.ActivateCutscene();
        StartCoroutine("explode");
    }
    IEnumerator explode()
    {
        yield return new WaitForSeconds(8.5f);
        psExplosion.Play();
        yield return new WaitForSeconds(8.5f);
        endScreen.SetActive(true);

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
