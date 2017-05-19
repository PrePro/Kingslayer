using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Flee : AI_Base
{
    public GameObject Player;

    float distance;
    public override float CalValue()
    {

        distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if(distance <= 5)
        {
            return 1f;
        }
        else if(distance > 5 && distance <= 10)
        {
            return 0.5f;
        }
        else
        {
            return 0.01f;
        }

    }

    public override void Run()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - Player.position);

        //Vector3 runTo = transform.position + transform.forward * multiplyBy;

       //NavMeshHit hit;
       //NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default"));
       //
       //myNMagent.SetDestination(hit.position);
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }
}
