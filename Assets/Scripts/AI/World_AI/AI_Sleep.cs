using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sleep : AI_Base
{
    [Tooltip("the place where they want to sleep")]
    public GameObject Target;
    [Tooltip("The return point when they are done sleeping")]
    public GameObject ReturnPoint;


    private float val;
    [Tooltip("tweek value example 2 will make the npc want to sleep double")]
    public float tweek;
    [Tooltip("how long the npc will sleep for")]
    public float TimeAway;
    [Header("Debugging")]
    public float sleep;

    float distance;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= TimeAway)
        {
            sleep += 1;
            timer = 0;
        }
    }

    public override float CalValue()
    {
        distance = Vector3.Distance(Target.transform.position, this.transform.position);

        float sleepFactor = Mathf.Clamp01(sleep / 50f);
        float distanceFactor = Mathf.Clamp01(distance / 50f);
        return (Mathf.InverseLerp(0, 10, (tweek * (sleepFactor / distanceFactor))));
    }

    public override void Run()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= 2f)
        {
            Debug.Log("Got home");
            gameObject.transform.position = new Vector3(1000, 0, 1000);
            StartCoroutine("Disable", 5);
        }
    }

    public override void Enter()
    {
        agent.SetDestination(Target.transform.position);
    }

    public override void Exit()
    {
    }

    IEnumerator Disable(float waitTime) // Move the object for sleep so it seems they disapper
    {
        sleep = 0;
        yield return new WaitForSeconds(waitTime);
        gameObject.transform.position = ReturnPoint.transform.position;
    }

    }
