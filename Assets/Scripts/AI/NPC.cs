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


    private float timer;


    void Update()
    {
        timer += Time.deltaTime;
        if (debuffState == Debuff.None)
        {
            if (stats.Death == false)
            {
                RunBehavior();
            }
            else
            {
                Death();
            }
        }
        else
        {
            HandleDebuff();
        }
    }

    bool isAttacking = false;
    bool isFacing = false;
    bool isInRange = false;
    //======================================================================================================
    // Function to run specific behavior on state change 
    //======================================================================================================
    IEnumerator RotateFind() // Turn enemy to find player
    {
        float totalTime = 0.0f;
        while (totalTime < 5.0f)
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
                    if (unitClass == UnitClass.Archer)
                    {


                    }
                    else
                    {
                        agent.Stop();
                        //agent.destination = transform.position;

                        SetAnimation(AnimationState.Idle);
                        if (currentState == State.Searching)
                        {
                            StartCoroutine(RotateFind());
                        }
                    }

                }
                break;
            case State.Attacking:
                {
                    SetAnimation(AnimationState.Attacking);
                    //enemySlash.Play();
                    agent.Stop();
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
                    if (dominantBehavior == Behavior.IdleDefencive)
                    {
                        Debug.Log("IdleDefencive");
                        agent.destination = startPosition;

                    }
                    else
                    {
                        agent.destination = currentTarget.position;
                    }
                }
                break;
            case State.Dead:
                {
                    //SetAnimation(AnimationState.Idle); // Set to death animation
                }
                break;
        }
        previousState = currentState;
        currentState = newState;
    }
    IEnumerator AIWait(float time)
    {
        yield return new WaitForSeconds(time);
        agent.destination = patrolRoute[patrolIndex].position;
        agent.Resume();

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
            case State.Dead:
                {
                    Debug.Log("HELPFDGLKNADGFA");
                    Death();
                }
                break;
        }

    }

    void Death()
    {
        SetAnimation(AnimationState.Dead);
        agent.Stop();
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
            SetState(State.Attacking);
            //AttackTarget();
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
            //Debug.Log("IsFacing");
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
            if (timer >= attackSpeed)
            {
                //Debug.Log("isAttacking = " + isAttacking);
                //Debug.Log("isFacing = " + isFacing);
                //Debug.Log("isInRange = " + isInRange);
                isFacing = true;
                if (!isAttacking && isFacing && isInRange)
                {
                    isAttacking = true;
                    SetAnimation(AnimationState.Attacking);
                    StartCoroutine(OnCompleteAttackAnimation());
                    timer = 0;
                    isAttacking = false;
                }
            }
            else
            {
                if (isFacing && isInRange)
                    SetAnimation(AnimationState.Idle);
            }
        }

        else
        {
            if (!isAttacking && isFacing && isInRange)
            {
                Debug.Log("Archer Attack");

                agent.Stop();
                isAttacking = true;

                //SetAnimation(AnimationState.Attacking);
                Shoot();
            }
        }
    }

    IEnumerator OnCompleteAttackAnimation()
    {
        Debug.Log("Start ATTACKING");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("sword_and_shield_slash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f);
        Debug.Log("DONE ATTACKING");
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

            if (HasWaitTime == true)  // Enemy Stop here to search 
            {
                int ran = UnityEngine.Random.Range(0, 11);
                if (ran >= 2) //80% change to stop and wait
                {
                    agent.Stop();
                    //YASH if you want to run an animation for the partrol do it here
                    int waitTime = UnityEngine.Random.Range(0, 11);
                    StartCoroutine(AIWait(waitTime));
                }
                else
                {
                    agent.destination = patrolRoute[patrolIndex].position;
                }

            }
            else
            {
                agent.destination = patrolRoute[patrolIndex].position;
            }

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

    public override void SetAnimation(AnimationState animState)
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
        isTargetSeen = false;

        if (unitClass == UnitClass.Archer)
        {
            SetAnimation(AnimationState.Idle);
            SetState(State.Idle);
        }
        if (currentState == State.Chasing || currentState == State.Attacking)
        {
            Debug.Log("Searching");
            SetState(State.Searching);
        }

    }
    #endregion

}