using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCityMarket : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("help");
            SceneManager.LoadScene("CityNight");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
