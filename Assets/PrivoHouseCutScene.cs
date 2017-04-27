using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivoHouseCutScene : MonoBehaviour
{

    public QuickCutsceneController houseCutscene;
    public bool houseCutsceneRunning = false;
    public Camera cinematicCamera;
    

    // Use this for initialization
    void Start()
    {

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
        
        yield return new WaitForSecondsRealtime(20f);
        houseCutscene.EndCutscene();
        cinematicCamera.enabled = false;
        //houseCutsceneRunning = true;
       
    }
}