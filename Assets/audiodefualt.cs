using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiodefualt : MonoBehaviour {
    public AudioSource audioSource;
    public AudioSource combatSource;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (audioSource.volume <= .5)
        {
            audioSource.volume += 0.005f;
        }
        if (combatSource.volume >= 0)
        {
            combatSource.volume -= 0.005f;
        }

    }
}
