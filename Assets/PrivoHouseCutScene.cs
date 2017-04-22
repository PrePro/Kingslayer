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
            houseCutsceneRunning = true;

        }
    }
}