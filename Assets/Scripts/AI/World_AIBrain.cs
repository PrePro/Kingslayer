using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_AIBrain : MonoBehaviour
{
    private bool canRun;
    AI_Sleep Sleep;

    void Start()
    {
        foreach (var component in GetComponents<Component>())
        {
            if(component is AI_Base)
            {
                if(component is AI_Sleep)
                {
                    if(Sleep == null)
                    {
                        Debug.Log("Got component");
                        Sleep = component.GetComponent<AI_Sleep>();
                    }
                }
            }
        }
    }

    void RunLogic()
    {
       //  sleep 10 
       // 5 30 
    }


    void Update()
    {
        if(canRun == true)
        {

        }
    }

    public void TurnOnBrain()
    {
        canRun = true;
    }
}
