using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCollection : MonoBehaviour
{
    public Camera gateCam;
    public ParticleSystem CupParticle;
    public static int cupCount = 0;
    public GameObject gate;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    //public GameObject respawnPoint;
   // private GameObject startPoint;

    private bool timetoend;

    void Start ()
    {
        gateCam.enabled = false;
        gate.SetActive(false);
        enemy1.SetActive(false);
        enemy2.SetActive(false);
        enemy3.SetActive(false);
        enemy4.SetActive(false);
        enemy5.SetActive(false);
        enemy6.SetActive(false);
    }
	
	void Update ()
    {
        if (gateCam.enabled == true)
        {
            //enemy1.SetActive(true);
            gate.SetActive(true);
            StartCoroutine("GateOpen");
        }
        if(timetoend == true)
        {
            Destroy(gameObject);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("e"))
            { 
                cupCount += 1;
                CupParticle.Play();
                gateCam.enabled = true;
            }
        }
    }

    IEnumerator GateOpen()
    {
        yield return new WaitForSeconds(1.5f);
        gateCam.enabled = false;
        enemy2.SetActive(true);
        enemy3.SetActive(true);
        enemy4.SetActive(true);
        enemy5.SetActive(true);
        enemy6.SetActive(true);
        timetoend = true;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
