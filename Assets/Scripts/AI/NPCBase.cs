//======================================================================================================
// EnemyBase.cs
// Description: 
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class NPCBase : MonoBehaviour
{


    //======================================================================================================
    // Datatype Declaration
    //======================================================================================================

    public enum State
    {
        Idle,
        Patrolling,
        FollowingPath,
        Chasing,
        Attacking,
        Searching,
        Dead,
        Disabled
    }

    public enum Debuff
    {
        None,
        Disabled,
        Stunned,
        Rooted
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
        Dead,
        Attack1,
        Attack2,
        Attack3,
        HitFlinch,
        ParryStagger,
        AOEKnockBack,
        Flee,
        Food,
        Patrol,
        Seek,
        Sleep,
        Wander,
        Work
    }

    public enum Behavior
    {
        Passive,  //Passive walk around cant be killed 
        Aggressive,
        IdleDefencive,
        PatrolDefencive,
        IdleAggressive,
        PatrolAggressive
    }
    public enum UnitClass
    {
        Knight,
        Archer,
        WorldAI
    }
    //======================================================================================================
    // Member Variables
    //======================================================================================================
    #region MemberVariables
    [Header("AI States")]
    [SerializeField]
    public UnitClass unitClass;
    [SerializeField]
    [Tooltip("The basic fallback behavior of the NPC: \nPassive units do not attack\nAggressive units search for enemies\nIdleDefencives units do not move but attack enemies in their radius\nPatrolDefencive units follow a patrol path until an enemy is found")]
    public Behavior dominantBehavior;
    //[SerializeField]
    //[Tooltip("Set the faction of the NPC to determine whether the unit is allied, neutral, or and enemy")]
    //protected Faction faction;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected NPStats stats;

    [SerializeField]
    [Tooltip("Should be turned on if you want the npc to die")]
    public bool canDie;

    //Debugging
    [Header("Debugging")]
    [SerializeField]
    [Tooltip("For debugging purposes only, should not be edited unless for testing purposes")]
    protected State currentState;
    protected State previousState;
    protected Debuff debuffState;
    [SerializeField]
    [Tooltip("For debugging purposes only, should not be edited unless for testing purposes")]
    protected AnimationState currentAnimation;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public bool isTargetSeen;
    [HideInInspector]
    public Animator animator;
    protected Vector3 startPosition;
    protected Quaternion startRotation;

    [Header("ParticleSystem")]
    protected ParticleSystem enemySlash;
    
    protected AI_KnightAttack KnightAttack;
    protected AI_ArcherAttack ArcherAttack;
    protected AI_Patrol Patroler;
    protected World_AIBrain Brain;
    protected AI_Death AI_mDeath;
    protected Rigidbody mRigidbody;
    #endregion
    //======================================================================================================
    // Properties
    //======================================================================================================
    #region Properties
    public State CurrentState
    {
        get { return currentState; }
    }


    #endregion

    //======================================================================================================
    // GameObject Functions
    //======================================================================================================
    #region GameObject Functions
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<NPStats>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        animator = GetComponent<Animator>();
        isTargetSeen = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if(canDie)
        {
            AI_mDeath = GetComponent<AI_Death>();
        }

        switch (unitClass)
        {
            case UnitClass.Knight:
                {
                    KnightAttack = GetComponent<AI_KnightAttack>();
                    if (dominantBehavior == Behavior.PatrolDefencive)
                    {
                        Patroler = GetComponent<AI_Patrol>();
                        if (Patroler == null)
                        {
                            Debug.Log("Patroler has no Script");
                        }
                    }

                    if (KnightAttack == null)
                    {
                        Debug.Log("KnightAttack has no Script");
                    }
                }
                break;
            case UnitClass.Archer:
                {
                    ArcherAttack = GetComponent<AI_ArcherAttack>();
                    if (ArcherAttack == null)
                    {
                        Debug.Log("ArcherAttack has no Script");
                    }
                }
                break;
            case UnitClass.WorldAI:
                {
                    Brain = GetComponent<World_AIBrain>();
                    if (Brain == null)
                    {
                        Debug.Log("Brain has no Script");
                    }
                }
                break;
            default:
                break;
        }

        switch (dominantBehavior)
        {
            case Behavior.Aggressive:
                {
                    SetState(State.Idle);
                    animator.SetInteger("AnimationState", (int)AnimationState.Idle);
                }
                break;
            case Behavior.IdleDefencive:
                {
                    SetState(State.Idle);
                    animator.SetInteger("AnimationState", (int)AnimationState.Idle);
                }
                break;
            case Behavior.PatrolDefencive:
                {
                    SetState(State.Patrolling);
                    animator.SetInteger("AnimationState", (int)AnimationState.Walking);
                }
                break;
            case Behavior.IdleAggressive:
                {
                    SetState(State.Idle);
                    animator.SetInteger("AnimationState", (int)AnimationState.Idle);
                }
                break;
            case Behavior.PatrolAggressive:
                {
                    SetState(State.Patrolling);
                    animator.SetInteger("AnimationState", (int)AnimationState.Walking);
                }
                break;
            case Behavior.Passive:
                {
                    if (unitClass == UnitClass.WorldAI)
                    {
                        Brain.TurnOnBrain();
                        SetState(State.Idle);
                        animator.SetInteger("AnimationState", (int)AnimationState.Idle);
                    }
                    else
                    {
                        SetState(State.Idle);
                        animator.SetInteger("AnimationState", (int)AnimationState.Idle);
                    }

                }
                break;
        }
    }

    #endregion
    //======================================================================================================
    // Abstract Member Functions
    //======================================================================================================
    #region Abstract Member Functions

    public abstract void RunBehavior();
    public abstract void SetState(State newState);
    public abstract void SetAnimation(AnimationState animState);
    public abstract void UpdateAnimation();
    public abstract void ChaseTarget(float range);
    //public abstract void AttackTarget();
    public abstract void Search();
    public abstract void HandleDebuff();
    public abstract void OnTargetFound(GameObject foundObject);
    public abstract void OnTargetLost();
    #endregion
    //======================================================================================================
    // Private Member Functions 
    //======================================================================================================
    #region Member Functions




    #endregion


    //======================================================================================================
    // Editor Rendering 
    //======================================================================================================

    public void OnDrawGizmos()
    {
        //DrawPerceptionGizmo();
        DrawBehaviorGizmo();
    }

    //Gives information what is going on in the ai
    private void DrawBehaviorGizmo()
    {
        if (!agent)
        {
            return;
        }

        switch (currentState)
        {
            case State.Chasing:
                Gizmos.color = new Color(1.0f, 132.0f / 255.0f, 0.0f);
                break;
            case State.Searching:
                Gizmos.color = Color.yellow;
                break;
            case State.Attacking:
                Gizmos.color = Color.red;
                break;
            case State.Patrolling:
                Gizmos.color = Color.magenta;
                break;
            case State.Idle:
                //Gizmos.color = Color.green;
                break;
            default:
                //Gizmos.color = Color.green;
                break;
        }

        Gizmos.DrawSphere(transform.position + Vector3.up * 4, 0.50f);
    }

    //shows perspective radius
    //void DrawPerceptionGizmo()
    //{

    //}

}