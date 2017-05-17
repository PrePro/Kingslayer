using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : AI_BaseAttack
{
    [SerializeField]
    [Tooltip("If you want to create a patrolling guard, set up an array of transforms for the unit to move back and forth")]
    protected List<Transform> patrolRoute;
    protected int patrolIndex;

    [SerializeField]
    [Tooltip("Distance the NPC needs to be to the current patrol point before moving to the next")]
    protected float patrolDistanceThreshold;


    [Tooltip("Patrolling has a random chance to wait at points")]
    [SerializeField]
    protected bool HasWaitTime;

    public override void Run()
    {
        Patrol();
    }

    public override void Enter()
    {
        //SetAnimation(AnimationState.Walking);
        if(npc == null)
        {
            Debug.Log("FUCK YOU");
        }
        npc.SetAnimation(NPCBase.AnimationState.Walking);
        agent.Resume();

        if (patrolRoute == null)
        {
            Debug.Log("Cannot patrol an empty patrol route");
            return;
        }
        var currIndex = patrolRoute.FindIndex(x => (x == npc.currentTarget));
        if (currIndex == -1)
        {
            patrolIndex = 0;
            //Find closed point
            for (int i = 0; i < patrolRoute.Count; ++i)
            {
                if (Vector3.Distance(transform.position, patrolRoute[i].position) <
                    Vector3.Distance(transform.position, patrolRoute[patrolIndex].position))
                {
                    patrolIndex = i;
                }
            }
            agent.destination = patrolRoute[patrolIndex].position;
        }
        npc.foundImage.SetActive(false);//put a yellow one. For Yash.
        npc.searchingImage.SetActive(false);
    }

    public override void Exit()
    {

    }

    IEnumerator AIWait(float time)
    {
        yield return new WaitForSeconds(time);
        agent.destination = patrolRoute[patrolIndex].position;
        agent.Resume();
    }

    public void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolRoute[patrolIndex].position) <= patrolDistanceThreshold)
        {
            patrolIndex++;
            if (patrolIndex >= patrolRoute.Count)
            {
                patrolIndex = 0;
            }

            if (HasWaitTime == true)  // Enemy Stop here to search 
            {
                int ran = UnityEngine.Random.Range(0, 11);
                if (ran >= 2) //80% change to stop and wait
                {
                    agent.Stop();
                    //SetAnimation(AnimationState.Idle);
                    npc.SetAnimation(NPCBase.AnimationState.Idle);
                    //YASH if you want to run an animation for the partrol do it here
                    int waitTime = UnityEngine.Random.Range(0, 11);
                    StartCoroutine(AIWait(waitTime));
                }
                else
                {

                    agent.destination = patrolRoute[patrolIndex].position;
                    npc.SetAnimation(NPCBase.AnimationState.Walking);
                    //SetAnimation(AnimationState.Walking);
                }

            }
            else
            {
                agent.destination = patrolRoute[patrolIndex].position;
            }

        }

    }

}
