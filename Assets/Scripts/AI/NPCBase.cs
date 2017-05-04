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
        HitFlinch
    }

    protected enum Behavior
    {
        Passive,  //Passive walk around cant be killed 
        Aggressive,
        IdleDefencive,
        PatrolDefencive
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
    protected UnitClass unitClass;
    [SerializeField]
    [Tooltip("The basic fallback behavior of the NPC: \nPassive units do not attack\nAggressive units search for enemies\nIdleDefencives units do not move but attack enemies in their radius\nPatrolDefencive units follow a patrol path until an enemy is found")]
    protected Behavior dominantBehavior;
    //[SerializeField]
    //[Tooltip("Set the faction of the NPC to determine whether the unit is allied, neutral, or and enemy")]
    //protected Faction faction;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected NPStats stats;

    //Debugging
    [SerializeField]
    [Tooltip("For debugging purposes only, should not be edited unless for testing purposes")]
    protected State currentState;
    protected State previousState;
    protected Debuff debuffState;
    [SerializeField]
    [Tooltip("For debugging purposes only, should not be edited unless for testing purposes")]
    protected AnimationState currentAnimation;

    [Header("Pathing")]
    protected Transform currentTarget;
    [SerializeField]
    [Tooltip("If you want to create a patrolling guard, set up an array of transforms for the unit to move back and forth")]
    protected List<Transform> patrolRoute;
    [Tooltip("Patrolling has a random chance to wait at points")]
    [SerializeField]
    protected bool HasWaitTime;
    protected int patrolIndex;
    [SerializeField]
    [Tooltip("Distance the NPC needs to be to the current patrol point before moving to the next")]
    protected float patrolDistanceThreshold;

    protected bool isTargetSeen;
    [Header("Attacking")]
    [SerializeField]
    [Tooltip("This really shouldnt be touched unless new Enemy")]
    protected float attackRange;
    [Tooltip("How fast the enemy attacks")]
    [SerializeField]
    protected float attackSpeed;

    protected Animator animator;
    protected Vector3 startPosition;

    [Header("ParticleSystem")]
    protected ParticleSystem enemySlash;

    [Header("Archer")]
    [Tooltip("bullet Object to be fired")]
    [SerializeField]
    protected GameObject Bullet;
    [Tooltip("Where the bulllet is shot from")]
    [SerializeField]
    protected GameObject BulletTarget;
    [Tooltip("How fast the archer attacks")]
    [SerializeField]
    protected float bulletSpeed;

    [SerializeField]
    protected GameObject DeathBox;
    protected World_AIBrain Brain;
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
        stats = GetComponent<NPStats>();
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        isTargetSeen = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
                }
                break;
            case Behavior.Passive:
                { 
                    if(unitClass == UnitClass.WorldAI)
                    {
                        Brain = GetComponent<World_AIBrain>();
                        if(Brain == null)
                        {
                            Debug.Log("Brain has no Script");
                        }
                        else
                        {
                            Brain.TurnOnBrain();
                        }
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
    public abstract void ChaseTarget();
    public abstract void AttackTarget();
    public abstract void Patrol();
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
        DrawPerceptionGizmo();
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
                Gizmos.color = Color.green;
                break;
            default:
                Gizmos.color = Color.green;
                break;
        }

        Gizmos.DrawSphere(transform.position + Vector3.up * 4, 0.50f);
    }

    //shows perspective radius
    void DrawPerceptionGizmo()
    {

    }

}