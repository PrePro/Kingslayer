using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Seek : AI_Base
{

    public GameObject Player;
    CoolDownSystem CdSystem;

    float distance;
    bool SwordIn;

    float speed;
    public float RunSpeed;
    public float TurnSpeed;

    void Awake()
    {
        CdSystem = Player.GetComponent<CoolDownSystem>();
    }

    void Update()
    {
        if (CdSystem.currentAnimState == CoolDownSystem.PlayerState.SwordInSheeth)
        {
            SwordIn = true;
        }
        else
        {
            SwordIn = false;
        }
    }

    public override float CalValue()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (SwordIn)
        {
            if (distance <= 8)
            {
                return 1f;
            }
            else
            {
                return 0.01f;
            }
        }
        else
        {
            return 0.01f;
        }


    }

    public override void Run()
    {
        //Vector3 d = (Player.transform.position - transform.position);

        if (distance >= 2)
        {
            Vector3 target = Player.transform.position;
            target.y = transform.position.y;
            target = target - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, target, TurnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            agent.isStopped = true;
        }


    }

    public override void Enter()
    {
        speed = agent.speed;
        agent.speed = RunSpeed;
    }

    public override void Exit()
    {
        agent.speed = speed;
        agent.isStopped = false;
    }
}
