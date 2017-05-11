using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Wander : AI_Base
{
    Vector3 target;
    float timer;
    public float newtargetTimer;

    //void Start()
    //{
    //    timer = newtargetTimer;
    //}

    void NewTarget()
    {
        Debug.Log("Wander");
        float myX = this.transform.position.x;
        float myZ = this.transform.position.z;

        float xPos = myX + Random.onUnitSphere.x * 20;
        float zPos = myZ + Random.onUnitSphere.y * 20;
        target = new Vector3(xPos, transform.position.y, zPos);
        //Debug.Log(target);
        agent.SetDestination(target);
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
