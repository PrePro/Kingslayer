using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Food : AI_Base
{
    [Tooltip("The place where the npc will go eat")]
    public GameObject Target;

    private float val;
    [Tooltip("tweek value example 2 will make the npc want to sleep double")]
    public float tweek;
    [Tooltip("How long untill food is added")]
    public float FoodTime;
    [Header("Debugging")]
    public float Food;

    float distance;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= FoodTime)
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

        return (Mathf.InverseLerp(0, 10, (tweek * (foodFactor / distanceFactor))));
    }

    public override void Run()
    {

        //Debug.Log(Vector3.Distance(this.transform.position, agent.destination));
        if (Vector3.Distance(transform.position, agent.destination) <= 3f)
        {
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
