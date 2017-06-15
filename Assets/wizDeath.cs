using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizDeath : MonoBehaviour {
    private float health;
    private bool done = false;
    public GameObject gate;
    public GameObject gate2;
    public GameObject gate3;
    public GameObject cam1;

	// Use this for initialization
	void Start ()
    {
        cam1.SetActive(false);
        health = gameObject.GetComponent<WizardBoss>().mCurrentHealth;
        gate2.SetActive(true);
        gate3.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        health = gameObject.GetComponent<WizardBoss>().mCurrentHealth;
        if (health <= 0 && done == false)
        {
            done = true;
            StartCoroutine("firstOne");
        }
		
	}
    IEnumerator firstOne()
    {
        cam1.SetActive(true);
        yield return new WaitForSeconds(2f);
        cam1.SetActive(false);
        gate.SetActive(false);
        gate2.SetActive(false);
        gate3.SetActive(false);
    }
}
