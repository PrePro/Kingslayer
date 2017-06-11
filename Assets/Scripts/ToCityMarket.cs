using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCityMarket : MonoBehaviour {
    public GameObject transScreen;
    public Movement movement; 
    // Use this for initialization
    void Start()
    {
        transScreen.SetActive(false);

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
    // Update is called once per frame
    void Update()
    {

    }
}
