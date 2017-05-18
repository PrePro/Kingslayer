using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBaseTesting : MonoBehaviour
{
    static public bool TurnOn;

    void Start()
    {

    }

    void OnTriggerEnter()
    {
        TurnOn = true;
        SceneManager.LoadScene("BaseTestingWorld");
    }

}
