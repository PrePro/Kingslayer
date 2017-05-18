using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sleep : AI_Base
{
    public GameObject Target;
    public GameObject ReturnPoint;

    public float sleep;
    private float val;
    public float tweek;
    public float TimeAway;

    public float min;
    public float max;

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
        //Debug.Log("SleepF : " + sleepFactor);
        float distanceFactor = Mathf.Clamp01(distance / 50f);
        //Debug.Log("DistanceF : " + distanceFactor);
        //Debug.Log(Mathf.InverseLerp(min, max, (tweek * (sleepFactor / distanceFactor))));

        return (Mathf.InverseLerp(min, max, (tweek * (sleepFactor / distanceFactor))));
    }

    public override void Run()
    {

        //Debug.Log(Vector3.Distance(this.transform.position, agent.destination));
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
