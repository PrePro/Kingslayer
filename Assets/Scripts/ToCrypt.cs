using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCrypt : MonoBehaviour {
    private Animator myAnimator;
	// Use this for initialization
	void Start () {
        /*myAnimator = GetComponent<Animator>();
        if(myAnimator == null)
        {
            Debug.Log("Didnt get animator (ToCrypt)");
        }*/
	}
    void OnTriggerEnter()
    {
       
            Debug.Log("Didnt get animator (ToCrypt)");
            SceneManager.LoadScene("Crypt");
        
       
    }
	// Update is called once per frame
	void Update () {
		
	}
}
