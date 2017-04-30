using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_AI : MonoBehaviour
{
    public enum State
    {
        Idle,  // Wander Around
        Shop,  // Go to the Shop
        Talk,  // Stop and talk to the player
        Sleep, // Go home and sleep
        Work,  // gp to a place and work (Run work animation)
        Play,  // go to a place and play (Run play animation)
        Find,  // Find the player and talk with him
        Flee   // if they get hit from the player couple times run away from him
    }

    public enum AnimationState
    {
        Idle,
        Walking,
        Attacking,
        Running,
        Blocking,
        Disabled,
        Stunned,
        Rooted,
        Dead
    }

    [Header("General Stuff")]
    [Tooltip("How fast the npc moves")]
    public int speed;
    protected UnityEngine.AI.NavMeshAgent agent;
    [Tooltip("Debugging only dont touch")]
    public State currentState;
    protected State previousState;
    protected AnimationState currentAnimation;
    protected Animator animator;

    [Header("Sleep State Logic")]
    public GameObject Sleep_WayPoint;
    [Tooltip("How long until the npc will decide to go home")]
    public int SleepTimer; //How long until the npc will take a nap
    [Tooltip("How long the npc will be inside its house")]
    public float TimeAway; //How long the npc will "be inside its house
    private float mCooldown; //The time holder for the sleep logic
    bool SleepCallOnce = false;

    [Header("Wander or Idle Logic")]
    [Tooltip("How long it will take before the trap picks a new direction")]
    public float newtargetTimer;
    Vector3 target;
    float timer;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed;
    }

    void SetState(State state)
    {
        if (currentState == state)
        {
            Debug.Log("Resetting the same State");
            return;
        }

        switch (state)
        {
            case State.Idle:
                {
                    agent.speed = 1;
                }
                break;
            case State.Shop:
                break;
            case State.Talk:
                break;
            case State.Sleep:
                //SetAnimation(AnimationState.Walking);
                agent.speed = 3f;
                agent.SetDestination(Sleep_WayPoint.transform.position);
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
        previousState = currentState;
        currentState = state;
    }

    void RunBehavior()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                    timer += Time.deltaTime;
                    if (timer >= newtargetTimer)
                    {
                        NewTarget();
                        timer = 0;
                    }
                }
                break;
            case State.Shop:
                //agent.SetDestination(Food_WayPoint.transform.position);
                break;
            case State.Talk:
                break;
            case State.Sleep:
                {
                    if (Vector3.Distance(transform.position, agent.destination) <= 1f)
                    {
                        StartCoroutine("Disable", TimeAway);
                    }
                }
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
        if (mCooldown <= SleepTimer + 2f)
        {
            mCooldown += Time.deltaTime;
        }

        if(mCooldown >= SleepTimer && SleepCallOnce == false)
        {
            SleepCallOnce = true;
            SetState(State.Sleep);
        }
    }

    void Update()
    {
        RunBehavior();
        SleepLogic();
    }

    void NewTarget()
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float xPos = myX + Random.Range(myX - 10, myX + 10);
        float zPos = myZ + Random.Range(myZ - 10, myZ + 10);

        target = new Vector3(xPos, transform.position.y, zPos);

        agent.SetDestination(target);
    }

    IEnumerator Disable(float waitTime) // Move the object for sleep so it seems they disapper
    { 
        gameObject.transform.position = new Vector3(1000, 0, 1000);
        yield return new WaitForSeconds(waitTime);
        mCooldown = 0;
        SleepCallOnce = false;
        gameObject.transform.position = Sleep_WayPoint.transform.position;
        SetState(State.Idle); // GET NEW LOGIC
    }

    #region Gizmos & Animation
    public void UpdateAnimation()
    {
        AnimationState currAnim = (AnimationState)animator.GetInteger("AnimationState");
        if (currentAnimation == AnimationState.Attacking && currAnim == AnimationState.Attacking)
        {
            animator.SetInteger("AnimationState", 0);
            //animator.SetInteger("AnimationState", (int)currentAnimation);
        }

        if (currentAnimation != currAnim)
        {
            animator.SetInteger("AnimationState", (int)currentAnimation);
        }
    }

    public void SetAnimation(AnimationState animState)
    {
        if (currentAnimation == AnimationState.Attacking && animState == AnimationState.Attacking)
        {
            //currentAnimation = animState;
            UpdateAnimation();
        }

        if (currentAnimation == animState)
        {
            return;
        }
        currentAnimation = animState;
        UpdateAnimation();
    }

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

        switch (currentState)
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