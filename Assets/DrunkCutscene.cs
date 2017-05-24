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

    // Use this for initialization
    void Start()
    {
        gameUI.SetActive(false);
        realDrunk.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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

        yield return new WaitForSecondsRealtime(13.6f);
        cineDelete = GameObject.FindGameObjectWithTag("CineStuff");
        Destroy(cineDelete);
        drunkCutscene.EndCutscene();
        cinematicCamera.enabled = false;
        drunkCutsceneRunning = true;
        gameUI.SetActive(true);
        realDrunk.SetActive(true);

    }
}
