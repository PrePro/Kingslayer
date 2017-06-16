using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToVillage : MonoBehaviour
    {
    public GameObject transScreen;
    public GameObject gate;
    private GameObject gateGuard;
    private NavMeshAgent agent;
    public Movement movement;
    // Use this for initialization
    void Start ()
    {
        transScreen.SetActive(false);
        gateGuard = GameObject.FindGameObjectWithTag("CineStuff");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transScreen.SetActive(true);
            StartCoroutine(movement.StopMovement(1f));
            StartCoroutine("fade");
        }
    }
    // Update is called once per frame
    void Update ()
    {
        gateGuard = GameObject.FindGameObjectWithTag("CineStuff");
        if (gateGuard == null)
        {
            gate.SetActive(false);
        }
	}
    IEnumerator fade()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Village1.1");
    }

}
