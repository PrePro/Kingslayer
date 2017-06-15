using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tofinalboss : MonoBehaviour {
    public GameObject transScreen;
    public Movement movement;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transScreen.SetActive(true);
            StartCoroutine(movement.StopMovement(2f));
            SceneManager.LoadScene("CityNight");
        }
    }
}
