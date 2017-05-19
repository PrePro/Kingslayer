using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Flee : AI_Base
{
    public GameObject Player;
    CoolDownSystem CdSystem;

    float distance;
    bool swordOut;

    float speed;
    public float RunSpeed;

    void Awake()
    {
        CdSystem = Player.GetComponent<CoolDownSystem>();
    }

    void Update()
    {
        if (CdSystem.currentAnimState == CoolDownSystem.PlayerState.SwordInHand)
        {
            swordOut = true;
        }
        else
        {
            swordOut = false;
        }
    }

    public override float CalValue()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (swordOut)
        {
            if (distance <= 5)
            {
                return 1f;
            }
            else if (distance > 5 && distance <= 10)
            {
                return 0.5f;
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
        Vector3 d = (transform.position - Player.transform.position);
        agent.SetDestination(d * 10);


        if (distance <= 5)
        {
            agent.speed = RunSpeed * 2;
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
    }
}
