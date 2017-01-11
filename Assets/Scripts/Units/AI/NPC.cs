﻿//======================================================================================================
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
    void FixedUpdate()
    {
        RunBehavior();
    }

    bool isAttacking = false;
    float attackSpeed = 20.0f;
    //======================================================================================================
    // Function to run specific behavior on state change 
    //======================================================================================================
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
                    SetAnimation(AnimationState.Idle);
                }
                break;
            case State.Attacking:
                {
                    Debug.Log("Attack Case");
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
                    agent.destination = currentTarget.position;
                }
                break;
        }
        currentState = newState;
    }


    //======================================================================================================
    // State machine set up to run behaviors based on AI's current behavior state
    //======================================================================================================
    #region Behaviors
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

        if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
        {
            SetState(State.Attacking);
        }
    }

    public override void AttackTarget()
    {
        Debug.Log("Attack Target Function Called");
        if (!isTargetSeen)
        {
            SetState(State.Searching);
        }

        if (Vector3.Distance(transform.position, currentTarget.position) > attackRange)
        {
            isAttacking = false;
            SetState(State.Chasing);
        }

        if (!isAttacking)
        {
            isAttacking = true;
            SetAnimation(AnimationState.Attacking);
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
        if(currentAnimation == animState)
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
        if (currentState == State.Chasing || currentState == State.Attacking)
        {
            SetState(State.Searching);
        }
    }
    #endregion

}
