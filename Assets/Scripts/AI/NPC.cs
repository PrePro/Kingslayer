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
    [Header("Images")]
    public GameObject foundImage;
    public GameObject searchingImage;
    private bool playerDead;

    void Update()
    {
        if (debuffState == Debuff.None)
        {
            if(dominantBehavior != Behavior.Passive)
            {
                if (stats.Death == false)
                {
                    playerDead = Player.isDead;
                    RunBehavior();
                }
                else
                {
                    AI_mDeath.Death();
                }
            }
            else
            {
                RunBehavior();
            }

        }
        else
        {
            HandleDebuff();
        }
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

                    agent.isStopped = true;

                    SetAnimation(AnimationState.Idle);

                    foundImage.SetActive(false);//put a yellow one. For Yash.
                    searchingImage.SetActive(false);


                }
                break;
            case State.Attacking:
                {
                    switch (unitClass)
                    {
                        case UnitClass.Knight:
                            {
                                KnightAttack.Enter();
                            }
                            break;
                        case UnitClass.Archer:
                            {
                                Debug.Log("SET ATTACKING");
                                ArcherAttack.Enter();
                            }
                            break;
                        case UnitClass.WorldAI:
                            break;
                        default:
                            break;
                    }

                }
                break;
            case State.Chasing:
                {
                    if (unitClass != UnitClass.Archer)
                    {
                        SetAnimation(AnimationState.Walking);
                        agent.isStopped = false;
                        agent.destination = currentTarget.position;
                        foundImage.SetActive(true);//put a yellow one. For Yash.
                        searchingImage.SetActive(false);
                    }
                }
                break;
            case State.Patrolling:
                {
                    Patroler.Enter();
                }
                break;
            case State.Searching:
                {
                    SetAnimation(AnimationState.Walking);
                    agent.isStopped = false;
                    if (dominantBehavior == Behavior.IdleDefencive || dominantBehavior == Behavior.IdleAggressive)
                    {
                        //Debug.Log("IdleDefencive");
                        agent.destination = startPosition;

                    }
                    else
                    {
                        if(playerDead)
                        {
                            SetState(State.Idle);
                        }
                        else
                        {
                            agent.destination = currentTarget.position;
                        }
                    }
                    foundImage.SetActive(false);//put a yellow one. For Yash.
                    searchingImage.SetActive(true);
                }
                break;
            case State.Dead:
                {
                    SetAnimation(AnimationState.Dead);
                    AI_mDeath.Enter();
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

    IEnumerator PushBack(float waitTime, Vector3 dir)
    {
        mRigidbody.AddForce(dir * 1000);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Set back 0");
        mRigidbody.velocity = Vector3.zero;

    }

    public void PushBack(Vector3 pos)
    {
        Vector3 dir = (transform.position - pos).normalized;

        StartCoroutine(PushBack(1f, dir));
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
                //agent.Stop();
                agent.isStopped = true;
                break;
            case Debuff.Rooted:


                switch (unitClass)
                {
                    case UnitClass.Knight:
                        {
                            if (GameplayStatics.IsWithinRange2D(transform, currentTarget.position, KnightAttack.attackRange) && isTargetSeen && dominantBehavior != Behavior.Passive)
                            {
                                KnightAttack.Run();
                            }
                        }
                        break;
                    case UnitClass.Archer:
                        {
                            Debug.Log("Archer Rooted");// Blind them
                        }
                        break;
                    case UnitClass.WorldAI:
                        break;
                    default:
                        break;
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
                    Patroler.Run();
                }
                break;
            case State.Attacking:
                {
                    //AttackTarget();
                    switch (unitClass)
                    {
                        case UnitClass.Knight:
                            {
                                KnightAttack.Run();
                            }
                            break;
                        case UnitClass.Archer:
                            {
                                ArcherAttack.Run();
                            }
                            break;
                        case UnitClass.WorldAI:
                            break;
                        default:
                            break;
                    }
                }
                break;
            case State.Chasing:
                {
                    switch (unitClass)
                    {
                        case UnitClass.Knight:
                            {
                                ChaseTarget(KnightAttack.attackRange);
                            }
                            break;
                        case UnitClass.Archer:
                            {

                            }
                            break;
                        case UnitClass.WorldAI:
                            break;
                        default:
                            break;
                    }
                }
                break;
            case State.Dead:
                {
                }
                break;
        }

    }


    //======================================================================================================
    // As long as the target is seen, update the target's new position 
    //======================================================================================================
    public override void ChaseTarget(float range)
    {
        if (isTargetSeen)
        {
            agent.destination = currentTarget.position;
        }

        if (GameplayStatics.IsWithinRange2D(transform, currentTarget.position, range, 1.0f))
        {
            SetState(State.Attacking);
            //AttackTarget();
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
                case Behavior.IdleAggressive:
                    {
                        SetState(State.Idle);
                    }
                    break;
                case Behavior.PatrolAggressive:
                    {
                        SetState(State.Patrolling);
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
        if (currentAnimation == AnimationState.Attacking && currAnim == AnimationState.Attacking) //|| currAnim == AnimationState.Attack1 || currAnim == AnimationState.Attack2 || currAnim == AnimationState.Attack3
        {
            Debug.Log("Fkc");
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
        if (stats.Death == false)
        {
            if (unitClass == UnitClass.Archer)
            {
                isTargetSeen = true;
                currentTarget = foundObject.transform;
                SetState(State.Attacking);
                return;
            }

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

    }

    //======================================================================================================
    // Function perception calls when a target can no longer be seen
    //======================================================================================================
    public override void OnTargetLost()
    {
        isTargetSeen = false;

        if (unitClass == UnitClass.Archer)
        {
            SetState(State.Idle);
        }
        if (currentState == State.Chasing || currentState == State.Attacking)
        {
           // Debug.Log("Searching");
            SetState(State.Searching);
        }

    }
    #endregion

}