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
	void Update ()
    {
	
	}

    //======================================================================================================
    //
    //======================================================================================================

    public override void AttackTarget()
    {
    }

    public override void RunBehavior()
    {
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
            agent.destination = currentTarget.position;
        }
    }
}
