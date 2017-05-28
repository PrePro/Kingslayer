using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCutsceneDrunk : MonoBehaviour {

    public GameObject player;
    public GameObject CinematicPlayer;
    public QuickCutsceneController guardCutscene;
    public bool GuardCutsceneRunning = false;
    public Camera cinematicCamera;
    public GameObject drunkGuard;
    public GameObject transformto;
    public GameObject HUD;
    
    // Use this for initialization
    void Start ()
    {
        cinematicCamera.enabled = false;
        CinematicPlayer.SetActive(false);
        drunkGuard.SetActive(false);
    }

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GuardCutsceneRunning == false)
            {
                GuardCutsceneRunning = true;
                HUD.gameObject.SetActive(false);
                cinematicCamera.enabled = true;
                player.SetActive(false);
                guardCutscene.ActivateCutscene();
                CinematicPlayer.SetActive(true);
                drunkGuard.SetActive(true);
                StartCoroutine(CinematicGuard());
            }
        }
        else
        {
            Debug.Log("ok");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator CinematicGuard()
    {
        yield return new WaitForSecondsRealtime(10f);
        GuardCutsceneRunning = true;
        player.transform.position = transformto.transform.position;
        CinematicPlayer.SetActive(false);
        guardCutscene.EndCutscene();
        cinematicCamera.enabled = false;
        player.SetActive(true);
        HUD.gameObject.SetActive(false);

    }
}
