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

                }
                break;
            case State.Patrolling:
                {

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
    }
}
