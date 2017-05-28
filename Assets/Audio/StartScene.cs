using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        AudioSource Audio = GetComponent<AudioSource>();
        Audio.PlayDelayed(1);
        Debug.Log(Audio);
    }
    // Update is called once per frame
    void Update () {
        

    }
}
