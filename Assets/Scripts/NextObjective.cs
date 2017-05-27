using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObjective : MonoBehaviour
{
    public CompassTurn cp;
    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            cp.GotoNextObjective();
        }
    }
}
