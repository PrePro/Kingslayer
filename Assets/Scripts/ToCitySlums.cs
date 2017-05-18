using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCitySlums : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }
    void OnTriggerEnter()
    {
        Debug.Log("help");
        SceneManager.LoadScene("CitySlum");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
