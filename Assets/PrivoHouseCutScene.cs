using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivoHouseCutScene : MonoBehaviour
{

    public QuickCutsceneController houseCutscene;
    public bool houseCutsceneRunning = false;
    

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
            Debug.Log("cutscene1:" + houseCutscene.playingCutscene);
        }
        if(houseCutscene.playingCutscene == true)
        {
            houseCutsceneRunning = true;
            Debug.Log("cutscene2:" + houseCutscene.playingCutscene);
            //ShouseCutscene.OnCutsceneEnd()
            houseCutscene.playingCutscene = false;
        }
        if(houseCutscene.playingCutscene == false && houseCutsceneRunning == true)
        {
            houseCutscene.EndCutscene();
            Debug.Log("cutscene3:"+ houseCutscene.playingCutscene);
            
        }
        
    }

    public void Endcutscene()
    {
        houseCutscene.EndCutscene();
    }
}