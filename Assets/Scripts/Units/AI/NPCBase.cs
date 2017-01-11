﻿//======================================================================================================
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
    public enum Faction
    {
        Allied,
        Enemy,
        Neutral
    }
    public enum State
    {
        Idle,
        Patrolling,
        Chasing,
        Attacking,
        Searching
    }

    protected enum AnimationState
    {
        Idle,
        Walking,
        Blocking,
        Attacking
    }

    protected enum Behavior
    {
        Passive,
        Aggressive,
        IdleDefencive,
        PatrolDefencive
    }
    public enum UnitClass
    {
        Knight,
        Archer,
        Mage,
        Healer
    }
    //======================================================================================================
    // Member Variables
    //======================================================================================================
    #region MemberVariables
    [Header("AI States")]
    [SerializeField]
    [Tooltip("The basic fallback behavior of the NPC: \nPassive units do not attack\nAggressive units search for enemies\nIdleDefencives units do not move but attack enemies in their radius\nPatrolDefencive units follow a patrol path until an enemy is found")]
    protected Behavior dominantBehavior;
    [SerializeField]
    [Tooltip("Set the faction of the NPC to determine whether the unit is allied, neutral, or and enemy")]
    protected Faction faction;
    protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    protected UnitClass unitClass;

    //Debugging
    [SerializeField]
    [Tooltip("For debugging purposes only, should not be edited unless for testing purposes")]
    protected State currentState;
    [SerializeField]
    [Tooltip("For debugging purposes only, should not be edited unless for testing purposes")]
    protected AnimationState currentAnimation;

    [Header("Pathing")]
    [SerializeField]
    protected Transform currentTarget;
    [SerializeField]
    [Tooltip("If you want to create a patrolling guard, set up an array of transforms for the unit to move back and forth")]
    protected List<Transform> patrolRoute;
    [SerializeField]
    protected int patrolIndex;
    [SerializeField]
    [Tooltip("Distance the NPC needs to be to the current patrol point before moving to the next")]
    protected float patrolDistanceThreshold;

    [Header("Perception")]
    [SerializeField]
    protected bool isTargetSeen;

    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected int attackDamage;
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
        isTargetSeen = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
            case Behavior.Passive:
                {
                    SetState(State.Idle);
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
    public abstract void UpdateAnimation();
    public abstract void ChaseTarget();
    public abstract void AttackTarget();
    public abstract void Patrol();
    public abstract void Search();
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
        if (isTargetSeen)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawSphere(transform.position + Vector3.up * 4, 0.50f);
    }

    //shows perspective radius
    void DrawPerceptionGizmo()
    {

    }

}
