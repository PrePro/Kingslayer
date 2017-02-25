//======================================================================================================
// BasicEnemy.cs
// Description: 
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;
using System;

public class NPC : NPCBase
{

    // Use this for initialization

    /*// For prototype animation
    [Header("Animation")]
    [Tooltip("...")]
    [SerializeField]
    private Animator myAnimator;
    public bool isFollow;
    public bool isAttack;
    */
    // Update is called once per frame

    [Header("Bullet")]
    [Tooltip("Only need to be added if you")]
    public GameObject Bullet;
    public GameObject BulletTarget;
    public float bulletSpeed;


    void Update()
    {
        //Debug.Log(debuffState);
        if (debuffState == Debuff.None)
        {
            //Debug.Log("RunBehavior");
            RunBehavior();
        }
        else
        {
            //Debug.Log("HandleDebuff");
            HandleDebuff();
        }
    }

    bool isAttacking = false;
    bool isFacing = false;
    bool isInRange = false;
    float attackSpeed = 10.0f;
    //======================================================================================================
    // Function to run specific behavior on state change 
    //======================================================================================================
    IEnumerator RotateFind() // Turn enemy to find player
    {
        float totalTime = 0.0f;
        while(totalTime < 5.0f)
        {
            //Debug.Log(totalTime);
            totalTime += Time.deltaTime;
            transform.Rotate(new Vector3(0, Time.deltaTime * 10, 0));
        }
        totalTime = 0.0f;
        while (totalTime < 10.0f)
        {
            //Debug.Log(totalTime);
            totalTime += Time.deltaTime;
            transform.Rotate(new Vector3(0, Time.deltaTime * -10, 0));
        }
        yield return null;
    }

    public override void SetState(State newState)
    {
        if (currentState == newState)
        {
            return;
        }
        switch (newState)
        {
            case State.Idle:
                {
                    agent.Stop();
                    //agent.destination = transform.position;

                    SetAnimation(AnimationState.Idle);
                    if(currentState == State.Searching)
                    {
                        StartCoroutine(RotateFind());
                    }

                }
                break;
            case State.Attacking:
                {
                    //Debug.Log("Attack Case");
                    agent.Stop();
                    // agent.destination = transform.position;
                }
                break;
            case State.Chasing:
                {
                    SetAnimation(AnimationState.Walking);
                    agent.Resume();
                    agent.destination = currentTarget.position;
                }
                break;
            case State.Patrolling:
                {
                    SetAnimation(AnimationState.Walking);
                    agent.Resume();

                    if (patrolRoute == null)
                    {
                        Debug.Log("Cannot patrol an empty patrol route");
                        return;
                    }
                    var currIndex = patrolRoute.FindIndex(x => (x == currentTarget));
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
                }
                break;
            case State.Searching:
                {
                    SetAnimation(AnimationState.Walking);
                    agent.Resume();

                    agent.destination = currentTarget.position;
                }
                break;
        }
        previousState = currentState;
        currentState = newState;
    }

    public IEnumerator RootAI(float time)
    {
        debuffState = Debuff.Rooted;
        yield return new WaitForSeconds(time);

        if (debuffState == Debuff.Rooted)
        {
            debuffState = Debuff.None;
        }
        yield return null;
    }

    public void startStunAI(float time)
    {
        StartCoroutine(StunAI(time));
    }

    public void startRootAI(float time)
    {
        StartCoroutine(RootAI(time));
    }


    public IEnumerator StunAI(float time)
    {
        Debug.Log("STUN AI");
        Debug.Log(time);
        debuffState = Debuff.Stunned;
        yield return new WaitForSeconds(time);

        Debug.Log("Changed Stunned");
        if (debuffState == Debuff.Stunned)
        {
            debuffState = Debuff.None;
        }
        Debug.Log("Done");
        //yield return null;
    }

    //======================================================================================================
    // State machine set up to run behaviors based on AI's current behavior state
    //======================================================================================================
    #region Behaviors
    public override void HandleDebuff()
    {
        switch (debuffState)
        {
            case Debuff.Stunned:
                agent.Stop();
                break;
            case Debuff.Rooted:

                if (GameplayStatics.IsWithinRange2D(transform, currentTarget.position, attackRange) && isTargetSeen && dominantBehavior != Behavior.Passive)
                {
                    AttackTarget();
                }
                break;
            default:
                break;
        }
    }


    public override void RunBehavior()
    {
        switch (currentState)
        {
            case State.Idle:
                {
                }
                break;
            case State.Searching:
                {
                    Search();
                }
                break;
            case State.Patrolling:
                {
                    Patrol();
                }
                break;
            case State.Attacking:
                {
                    AttackTarget();
                }
                break;
            case State.Chasing:
                {
                    ChaseTarget();
                }
                break;
        }

    }


    //======================================================================================================
    // As long as the target is seen, update the target's new position 
    //======================================================================================================
    public override void ChaseTarget()
    {
        if (isTargetSeen)
        {
            agent.destination = currentTarget.position;
        }

        if (GameplayStatics.IsWithinRange2D(transform, currentTarget.position, attackRange, 1.0f))
        {
            //Debug.Log("Within range");
            SetState(State.Attacking);
        }
    }

    public override void AttackTarget()
    {
        if (!GameplayStatics.IsFacing(transform, currentTarget.position) && isInRange)
        {
            isAttacking = false;
            isFacing = false;
            Vector3 target = currentTarget.position;
            target.y = transform.position.y;
            target = target - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, target, agent.angularSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else if (isInRange)
        {
            isFacing = true;
        }
        else
        {
            isFacing = false;
        }

        if (!GameplayStatics.IsWithinRange2D(transform, currentTarget.position, attackRange))
        {
            isAttacking = false;
            isInRange = false;
            if (!isTargetSeen)
            {
                SetState(State.Searching);
            }
            else
            {
                SetState(State.Chasing);
            }
        }
        else
        {
            isInRange = true;
        }
        if (unitClass == UnitClass.Knight)
        {


            if (!isAttacking && isFacing && isInRange)
            {
                isAttacking = true;
                SetAnimation(AnimationState.Attacking);
            }
        }
        else
        {
            if (!isAttacking && isFacing && isInRange)
            {
                Debug.Log("Archer Attack");
                isAttacking = true;
                //SetAnimation(AnimationState.Attacking);
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        Vector3 firePosition = BulletTarget.transform.position;
        GameObject bullet = GameObject.Instantiate(Bullet, firePosition, BulletTarget.transform.rotation) as GameObject;

        if (bullet != null)
        {
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * bulletSpeed;
            rigidbody.AddForce(force);
        }
    }
    //======================================================================================================
    // Iterate throught array of patrol paths 
    //======================================================================================================
    public override void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolRoute[patrolIndex].position) <= patrolDistanceThreshold)
        {
            patrolIndex++;
            if (patrolIndex >= patrolRoute.Count)
            {
                patrolIndex = 0;
            }
            agent.destination = patrolRoute[patrolIndex].position;
        }
    }

    public override void Search()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= 2.0f)
        {
            switch (dominantBehavior)
            {
                case Behavior.Aggressive:
                    {
                        SetState(State.Idle);
                    }
                    break;
                case Behavior.IdleDefencive:
                    {
                        SetState(State.Idle);
                    }
                    break;
                case Behavior.PatrolDefencive:
                    {
                        SetState(State.Patrolling);
                    }
                    break;
            }
        }
    }



    #endregion

    //======================================================================================================
    // TODO: To be filled out once we have models and animation
    //======================================================================================================
    public override void UpdateAnimation()
    {
        AnimationState currAnim = (AnimationState)animator.GetInteger("AnimationState");
        if (currentAnimation != currAnim)
        {
            animator.SetInteger("AnimationState", (int)currentAnimation);
        }
    }

    public override void SetAnimation(AnimationState animState)
    {
        if (currentAnimation == animState)
        {
            return;
        }
        currentAnimation = animState;
        UpdateAnimation();
    }


    #region Perception Updates
    //======================================================================================================
    // Function perception calls when a target has been found
    //======================================================================================================
    public override void OnTargetFound(GameObject foundObject)
    {
        if (dominantBehavior != Behavior.Passive)
        {
            currentTarget = foundObject.transform;
            if (currentState != State.Attacking)
            {
                SetState(State.Chasing);
            }
            isTargetSeen = true;
        }
    }

    //======================================================================================================
    // Function perception calls when a target can no longer be seen
    //======================================================================================================
    public override void OnTargetLost()
    {
        Debug.Log("lost Target");
        isTargetSeen = false;
        if (currentState == State.Chasing || currentState == State.Attacking)
        {
            Debug.Log("Searching");
            SetState(State.Searching);
        }
        
    }
    #endregion

}
