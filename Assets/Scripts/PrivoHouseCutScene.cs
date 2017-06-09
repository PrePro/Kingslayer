using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivoHouseCutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject CinematicPlayer;
    public QuickCutsceneController houseCutscene;
    public bool houseCutsceneRunning = false;
    public Camera cinematicCamera;
    public GameObject cineDelete;
    public GameObject questPopUp;
    public GameObject HUD;
    public GameObject lumenIcon;
    public GameObject fire;

    // Use this for initialization
    void Start()
    {
        CinematicPlayer.SetActive(true);
        player.SetActive(false);
        questPopUp.gameObject.SetActive(false);
        HUD.gameObject.SetActive(false);
        lumenIcon.gameObject.SetActive(false);
        fire.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (houseCutscene.playingCutscene == false && houseCutsceneRunning == false)
        {
            houseCutscene.ActivateCutscene();
            StartCoroutine(CinematicHouse());
            Debug.Log("cutscene1:" + houseCutscene.playingCutscene);
        }
        /*if(houseCutscene.playingCutscene == true)
        {
            
            Debug.Log("cutscene2:" + houseCutscene.playingCutscene);
            
            //houseCutsceneRunning = true;

            //ShouseCutscene.OnCutsceneEnd()
        }
        
        houseCutsceneRunning = false;
        if (houseCutsceneRunning == true)
        {
            

            Debug.Log("cutscene3:"+ houseCutscene.playingCutscene);
            
        }*/
        
    }

    public void Endcutscene()
    {
        houseCutscene.EndCutscene();
    }

    IEnumerator CinematicHouse()
    {
        
        yield return new WaitForSecondsRealtime(27f);
        player.SetActive(true);
        CinematicPlayer.SetActive(false);
        Destroy(cineDelete);
        houseCutscene.EndCutscene();
        cinematicCamera.enabled = false;
        questPopUp.gameObject.SetActive(true);
        HUD.gameObject.SetActive(true);
        lumenIcon.gameObject.SetActive(true);
        fire.gameObject.SetActive(true);
        houseCutsceneRunning = true;

    }
}