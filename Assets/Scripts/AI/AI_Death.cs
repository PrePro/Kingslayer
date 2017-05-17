using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Death : AI_BaseAttack
{
    public GameObject DeathBox;
    [Header("Death")]
    [Tooltip("This is where they go after they die")]
    public GameObject DeathWayPoint;
    [Tooltip("How long they wait before they get up and walk away")]
    public float DeathTimer;
    [Tooltip("How much morality the player gets for letting the npc live\nShould be positive")]
    public int MoralityForSaving;

    bool mDeath;


    public override void Run()
    {

    }
    public override void Enter()
    {
        DeathBox.SetActive(true);
        StartCoroutine("Death", DeathTimer);
    }
    public override void Exit()
    {

    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);

        PlayerStats stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        stats.Morality += MoralityForSaving;
        mDeath = true;
    }

    public void Death()
    {
        if (mDeath == true)
        {
            agent.Resume();
            npc.SetAnimation(NPCBase.AnimationState.Walking);
           // SetAnimation(AnimationState.Walking);

            agent.SetDestination(DeathWayPoint.transform.position); // Make this a gameObject

            if (Vector3.Distance(transform.position, agent.destination) <= 3f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            agent.Stop();
        }
    }
}
