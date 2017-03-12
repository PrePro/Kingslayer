using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchMenu : MonoBehaviour {
    public Camera cam1;
    public Camera cam2;

	// Use this for initialization
	void Awake () {
        cam1.enabled = true;
        cam2.enabled = false;
        //cam1.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    void switchCamera()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
    }
}
