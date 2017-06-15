using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkCutscene : MonoBehaviour {
    public QuickCutsceneController drunkCutscene;
    public bool drunkCutsceneRunning = false;
    public Camera cinematicCamera;
    public GameObject gameUI;
    private GameObject cineDelete;
    public GameObject realDrunk;
    public GameObject gate;
    public Movement move;
    private bool m;

    // Use this for initialization
    void Start()
    {
        gameUI.SetActive(false);
        realDrunk.SetActive(false);
        gate.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m == true)
        {
            move.stopMovement = true;
        }
        
        if (drunkCutscene.playingCutscene == false && drunkCutsceneRunning == false)
        {
            drunkCutscene.ActivateCutscene();
            StartCoroutine(CinematicHouse());
        }
    }

    public void Endcutscene()
    {
        drunkCutscene.EndCutscene();
    }

    IEnumerator CinematicHouse()
    {

        yield return new WaitForSecondsRealtime(20.6f);
        cineDelete = GameObject.FindGameObjectWithTag("CineStuff");
        Destroy(cineDelete);
        drunkCutscene.EndCutscene();
        cinematicCamera.enabled = false;
        drunkCutsceneRunning = true;
        gameUI.SetActive(true);
        realDrunk.SetActive(true);
        gate.SetActive(true);
        m = false;
        move.stopMovement = false;
    }
}
