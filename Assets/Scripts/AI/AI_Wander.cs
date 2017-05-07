using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Wander : AI_Base
{
    Vector3 target;
    float timer;
    public float newtargetTimer;

    void NewTarget()
    {
        float myX = this.transform.position.x;
        float myZ = this.transform.position.z;

        float xPos = myX + UnityEngine.Random.Range(myX - 10, myX + 10);
        float zPos = myZ + UnityEngine.Random.Range(myX - 10, myZ + 10);

        target = new Vector3(xPos, transform.position.y, zPos);
        Debug.Log(target);
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
    }

    public override void Exit()
    {
    }

}
