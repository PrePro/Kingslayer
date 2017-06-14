using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionTrigger : MonoBehaviour {
    public ParticleSystem psExplosion;
    private bool stopexp = false;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && stopexp == false)
        {
            psExplosion.Play();
            stopexp = true;
        }
    }
}
