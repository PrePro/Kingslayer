using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlashParticle : MonoBehaviour {

    public ParticleSystem slash;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void playSlash()
    {
        slash.Play();
    }
}
