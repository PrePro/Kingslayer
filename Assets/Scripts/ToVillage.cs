using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToVillage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter()
    {
        Debug.Log("help");
        SceneManager.LoadScene("Village1.1");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
