using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Death : AI_BaseAttack
{
    public GameObject DeathBox;
    [Header("Death")]
    [Tooltip("This is where they go after they die")]
    public GameObject DeathWayPoint;
    [Tooltip("How long they wait before they get up and walk away")]
    public float DeathTimer;
    public Image executeBar;
    public GameObject executeIcon;
    public GameObject Ragdoll;
    private Color goodColor = new Color32(73, 214,217,255);
    private Color evilColor = new Color32(233, 108, 33, 255);
    bool mDeath;


    public override void Run()
    {

    }
    public override void Enter()
    {
        DeathBox.SetActive(true);
        StartCoroutine("Death", DeathTimer);
        executeIcon.SetActive(true);

    }
    public override void Exit()
    {

    }

    IEnumerator Death(float time)
    {
        while (time > 0)
        {
            executeBar.fillAmount = time / DeathTimer;
            executeBar.color = Color.Lerp(goodColor, evilColor, (time / DeathTimer));
            time -= .1f;
            //Debug.Log(time);
            yield return new WaitForSeconds(.1f);
        }
        executeIcon.gameObject.SetActive(false);
        DeathBox.SetActive(false);
        //stats.Morality += MoralityForSaving;
        mDeath = true;

    }

    public void Death()
    {
        if (mDeath == true)
        {
            agent.isStopped = false;
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
            agent.isStopped = true;
        }
    }
}
