//======================================================================================================
// BasicEnemy.cs
// Description: 
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;
using System;

public class NPC : NPCBase
{

    // Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        RunBehavior();
	}

    //======================================================================================================
    //
    //======================================================================================================

    public override void AttackTarget()
    {
        if(isTargetSeen)
        {
            agent.destination = currentTarget.position;
        }
    }

    public override void RunBehavior()
    {
        switch(currentState)
        {
            case State.Idle:
                {

                }
                break;
            case State.Searching:
                {
                    Search();
                }
                break;
            case State.Patrolling:
                {
                    Patrol();
                }
                break;
            case State.Attacking:
                {
                    AttackTarget();
                }
                break;
        }

    }

    public override void Patrol()
    {
        if(Vector3.Distance(transform.position, patrolRoute[patrolIndex].position) <= patrolDistanceThreshold)
        {
            patrolIndex++;
            if(patrolIndex >= patrolRoute.Count)
            {
                patrolIndex = 0;
            }
            agent.destination = patrolRoute[patrolIndex].position;
        }
    }

    public override void UpdateAnimation()
    {
    }

    public override void OnTargetFound(GameObject foundObject)
    {
        if(dominantBehavior != Behavior.Passive)
        {
            currentTarget = foundObject.transform;
            currentState = State.Attacking;
            isTargetSeen = true;
        }
    }
    public override void OnTargetLost()
    {
        isTargetSeen = false;
        if(currentState == State.Attacking)
        {
            SetState(State.Searching);
        }
    }

    public override void SetState(State newState)
    {
        if(currentState == newState)
        {
            return;
        }
        switch(newState)
        {
            case State.Idle:
                {

                }
                break;
            case State.Attacking:
                {

                }
                break;
            case State.Patrolling:
                {
                    if(patrolRoute == null)
                    {
                        Debug.Log("Cannot patrol an empty patrol route");
                        return;
                    }
                    var currIndex = patrolRoute.FindIndex(x => (x == currentTarget));
                    if (currIndex == -1)
                    {
                        patrolIndex = 0;
                        //Find closed point
                        for (int i = 0; i < patrolRoute.Count; ++i)
                        {
                            if (Vector3.Distance(transform.position, patrolRoute[i].position) <
                                Vector3.Distance(transform.position, patrolRoute[patrolIndex].position))
                            {
                                patrolIndex = i;
                            }
                        }
                        agent.destination = patrolRoute[patrolIndex].position;
                    }
                }
                break;
            case State.Searching:
                {

                }
                break;
        }
        currentState = newState;
    }

    public override void Search()
    {
        if(Vector3.Distance(transform.position, agent.destination) <= 1.0f)
        {
            switch(dominantBehavior)
            {
                case Behavior.Aggressive:
                    {

                    }
                    break;
                case Behavior.IdleDefencive:
                    {

                    }
                    break;
                case Behavior.PatrolDefencive:
                    {
                        SetState(State.Patrolling);
                    }
                    break;
            }
        }
    }
}
