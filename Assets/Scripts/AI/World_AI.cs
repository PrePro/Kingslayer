using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_AI : MonoBehaviour
{
    public GameObject Food_WayPoint;
    public int speed;
    protected UnityEngine.AI.NavMeshAgent agent;
    public State mState;
    private float mCooldown;

    public enum State
    {
        Idle,
        Shop,
        Talk,
        Sleep,
        Work,
        Play,
        Find,
        Flee
    }

    int food = 10;
    public int Sleep = 5;
    // Use this for initialization
    void Start()
    {

        StartCoroutine("Disable", 2f);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        agent.speed = speed;
    }

    void SetState(State state)
    {
        if (mState == state)
        {
            Debug.Log("Resetting State");
            return;
        }
        switch (mState)
        {
            case State.Idle:
                break;
            case State.Shop:
               
                break;
            case State.Talk:
                break;
            case State.Sleep:
                //Set Walking Animation
                agent.SetDestination(Food_WayPoint.transform.position);
               
                break;
            case State.Work:
                break;
            case State.Play:
                break;
            case State.Find:
                break;
            case State.Flee:
                break;
            default:
                break;
        }
    }

    void RunBehavior()
    {
        switch (mState)
        {
            case State.Idle:
                break;
            case State.Shop:
                //agent.SetDestination(Food_WayPoint.transform.position);
                break;
            case State.Talk:
                break;
            case State.Sleep:
                SleepLogic();
                break;
            case State.Work:
                break;
            case State.Play:
                break;
            case State.Find:
                break;
            case State.Flee:
                break;
            default:
                break;
        }
    }

    void SleepLogic()
    {
        if (mCooldown <= 2f)
        {
            mCooldown += Time.deltaTime;
        }

        if (mCooldown >= 1f)
        {
            Sleep--;
            mCooldown = 0;
        }
        if (Sleep == 0)
        {
            SetState(State.Sleep);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RunBehavior(); 
    }

    IEnumerator Disable(float waitTime) // Move the object for sleep so it seems they disapper
    { 
        gameObject.transform.position = new Vector3(1000, 0, 1000);
        yield return new WaitForSeconds(waitTime);
        gameObject.transform.position = Food_WayPoint.transform.position;
    }

    #region Gizmos
    public void OnDrawGizmos()
    {
        DrawBehaviorGizmo();
    }

    private void DrawBehaviorGizmo()
    {
        if (!agent)
        {
            return;
        }

        switch (mState)
        {
            case State.Idle:
                Gizmos.color = Color.green;
                break;
            case State.Shop:
                Gizmos.color = Color.red;
                break;
            case State.Talk:
                Gizmos.color = Color.green;
                break;
            case State.Sleep:
                Gizmos.color = Color.green;
                break;
            case State.Work:
                Gizmos.color = Color.green;
                break;
            case State.Play:
                Gizmos.color = Color.green;
                break;
            case State.Find:
                Gizmos.color = Color.green;
                break;
            case State.Flee:
                Gizmos.color = Color.green;
                break;
            default:
                break;
        }
        Gizmos.DrawSphere(transform.position + Vector3.up * 4, 0.50f);
    }
    #endregion
}

/*
Shop/Eat: Go to Market Stalls and buy food/supplies from NPCs who are “working”

Talk: Engage in a conversation loop with each other. 

Sleep: Go “home”, vanish into buildings for a set time before exiting and engaging in a new behaviour loop.

Work: Go to market stalls/blacksmith forge etc, engage with NPCs who are “shopping”.

Play: Child NPCs chase one another around stopping from time to time to “play-fight”. 

Find Player: Seek player and trigger conversation. Return to other behaviour loop after exiting dialogue.

Go for a walk/run: Traverse around the current scenes baked navmesh. 

Flee: Run away from aggressive Enemy NPCs that are attacking player.
*/
