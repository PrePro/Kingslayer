﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_AIBrain : MonoBehaviour
{
    private bool canRun;
    List<AI_Base> AI_Behaviour;
    protected UnityEngine.AI.NavMeshAgent agent;
    float timer;

    [Tooltip("How long until the next Value check")]
    public float TimeAway;
    [Tooltip("Debugging Only")]
    public AI_Base current;

    void Start()
    {
        timer = TimeAway;
        AI_Behaviour = new List<AI_Base>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        foreach (var component in GetComponents<Component>())
        {
            if (component is AI_Base)
            {
                AI_Behaviour.Add(component as AI_Base);
            }
        }
    }

    //timer += Time.deltaTime;
    //    if (timer >= TimeAway)
    //    {
    //timer = 0;
    //    }
    void RunLogic()
    {
        Debug.Log(current);
        AI_Base MostDesired = null;
        float min = 0;

        timer += Time.deltaTime;
        if (timer >= TimeAway)
        {
            Debug.Log("Checker");
            foreach (var ai in AI_Behaviour)
            {
                if (ai.CalValue() > min)
                {
                    min = ai.CalValue();
                    MostDesired = ai;
                }
            }
        timer = 0;
    }

        if (MostDesired != null)
        {
            if(current == MostDesired)
            {
                //Debug.Log("Current == MostDesired");
                return;
            }
            else
            {
                if(current != null)
                {
                    current.Exit();
                }
                current = MostDesired;
                current.Enter();
            }

            Debug.Log(MostDesired);
        }

        if (current != null)
        {
            current.Run();
        }
    }


    void Update()
    {
        if (canRun == true)
        {
            RunLogic();
        }
    }

    public void TurnOnBrain()
    {
        canRun = true;
    }
}
