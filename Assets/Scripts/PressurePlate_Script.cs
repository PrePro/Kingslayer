using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate_Script : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PuzzleWall.goBackUp = true;
        }
    }

}
