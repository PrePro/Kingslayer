using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaliceUI : MonoBehaviour {
    public GameObject chaliceImage;

	// Use this for initialization
	void Start () {
        chaliceImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.E))
            {
                chaliceImage.SetActive(true);
            }
        }
    }
}
