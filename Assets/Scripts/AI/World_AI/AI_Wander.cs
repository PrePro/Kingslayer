using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI_Wander : AI_Base
{
    Vector3 target;
    float timer;
    [Tooltip("How long before the npc will choice a new place to walk")]
    public float newtargetTimer;
    
    void CalculatePath()
    {
        float myX = this.transform.position.x;
        float myZ = this.transform.position.z;

        float xPos = myX + Random.onUnitSphere.x * 20;
        float zPos = myZ + Random.onUnitSphere.y * 20;
        target = new Vector3(xPos, transform.position.y, zPos);
    }

    void NewTarget()
    {
        CalculatePath();

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target, path);
        if (path.status == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("Path was not reachable");
            //Put idle here
        }
        else
        {
            agent.SetDestination(target);
        }
    }

    public override float CalValue()
    {
        return 0.1f; // 0.1
    }

    public override void Run()
    {
        timer += Time.deltaTime;

        if (timer >= newtargetTimer)
        {
            NewTarget();
            timer = 0;
        }
    }
    public override void Enter()
    {
        timer = newtargetTimer;
    }

    public override void Exit()
    {
        //Destroy(Follow);
    }
}
