using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNightSlums : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }
    void OnTriggerEnter()
    {
        Debug.Log("help");
        SceneManager.LoadScene("CityNightSlum");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
