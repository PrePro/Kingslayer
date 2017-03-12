using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchMenu : MonoBehaviour {
    public GameObject cam1;
    public GameObject cam2;

	// Use this for initialization
	void Awake () {
        
        //cam2.gameObject.SetActive(false);
        //cam1.gameObject.SetActive(true);
        cam1.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void enableCamera1()
    {

        Debug.Log("enableCam1");
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    public void enableCamera2()
    {

        Debug.Log("enableCam2");
        cam1.SetActive(false);
        cam2.SetActive(true);
    }
}
