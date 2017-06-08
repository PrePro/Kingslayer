using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCollection : MonoBehaviour
{
    public Camera gateCam;
    public ParticleSystem CupParticle;
    public static int cupCount = 0;
    public GameObject gate;
    public GameObject[] enemy;

    private bool CallOnce = false;

    public GameObject respawnPoint;
    public GameObject startPoint; // the on the player

    private bool timetoend;

    void Start ()
    {
        //timetoend = false;
        gateCam.enabled = false;
        gate.SetActive(false);
        foreach(GameObject Enemy in enemy)
        {
            Enemy.SetActive(false);
        }

    }
	
	void Update ()
    {
        if (gateCam.enabled == true && !CallOnce)
        {
            Debug.Log("Called");
            StartCoroutine("GateOpen");
            CallOnce = true;
        }
        if (timetoend == true)
        {
            Destroy(this.gameObject);
        }
     
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.E))
            {
                CallOnce = false;
                startPoint.transform.position = respawnPoint.transform.position;
                cupCount += 1;
                CupParticle.Play();
                gateCam.enabled = true;
            }
        }
    }

    IEnumerator GateOpen()
    {
        enemy[0].SetActive(true);
        gate.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        gateCam.enabled = false;
        for (int i = 1; i < enemy.Length; i++)
        {
            enemy[i].SetActive(true);
        }

        timetoend = true;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
