using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBaseTesting : MonoBehaviour
{
    static public bool TurnOn;

    void Start()
    {
        //if(ToBaseTesting.TurnOn)
        //{
        //    //spawn player at location
        //    //player.tranform.location = gameObject;
        //    ToBaseTesting.TurnOn = false;
        //}
    }

    void OnTriggerEnter()
    {
        TurnOn = true;
        SceneManager.LoadScene("BaseTestingWorld");
    }

}
