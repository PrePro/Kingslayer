//======================================================================================================
// EnemyBase.cs
// Description: 
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyBase : MonoBehaviour
{
  

    //======================================================================================================
    // Datatype Declaration
    //======================================================================================================
    public enum State
    {
        Idle,
        Patrolling,
        Attacking,
        Searching
    }

    enum AnimationState
    {
        Idle,
        Walking,
        Blocking,
        Attacking
    }

    enum Behavior
    {
        Aggressive,
        Defencive,
        Guard
    }
    //======================================================================================================
    // Member Variables
    //======================================================================================================
    #region MemberVariables
    [Header("AI States")]
    [SerializeField]
    State currentState;
    [SerializeField]
    AnimationState currentAnimation;
    [SerializeField]
    Behavior dominantBehavior;
    NavMeshAgent agent;

    [SerializeField]
    Transform currentTarget;

    [Header("Perception")]
    [SerializeField]
    float viewRadius;
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
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(currentTarget.position);
    }

    #endregion
    //======================================================================================================
    // Abstract Member Functions
    //======================================================================================================
    #region Abstract Member Functions

    public abstract void RunBehavior();
    public abstract void UpdateAnimation();
    public abstract void AttackEnemy();
    public abstract void SearchForEnemy();
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
        if (agent.isOnNavMesh)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawSphere(transform.position + Vector3.up * 4, 0.50f);
    }
    
    //shows perspective radius
    void DrawPerceptionGizmo()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
      
    }
    
}
