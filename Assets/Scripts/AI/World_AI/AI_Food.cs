using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Food : AI_Base
{
    public GameObject Target;

    public float Food;
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
            Food += 1;
            timer = 0;
        }
    }

    public override float CalValue()
    {
        distance = Vector3.Distance(Target.transform.position, this.transform.position);

        float foodFactor = Mathf.Clamp01(Food / 50f);
        float distanceFactor = Mathf.Clamp01(distance / 50f);

        return (Mathf.InverseLerp(min, max, (tweek * (foodFactor / distanceFactor))));
    }

    public override void Run()
    {

        //Debug.Log(Vector3.Distance(this.transform.position, agent.destination));
        if (Vector3.Distance(transform.position, agent.destination) <= 3f)
        {
            Debug.Log("Got home");
            StartCoroutine("Disable", 3f);
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
        yield return new WaitForSeconds(waitTime);
        Food = 0;
    }

}
