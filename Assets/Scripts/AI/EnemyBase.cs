//======================================================================================================
// EnemyBase.cs
// Description: 
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    //======================================================================================================
    // Datatype Declaration
    //======================================================================================================
    public enum State
    {
        Idle,
        Patrolling,
        Attacking
    }

    enum AnimationState
    {
        Idle,
        Walking,
        Blocking,
        Attacking
    }
    //======================================================================================================
    // Member Variables
    //======================================================================================================
    #region MemberVariables
    [Header("AI States")]
    State currentState;
    AnimationState currentAnimation;

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
    void Start ()
    {
	
	}
    void Update ()
    {
	
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
}
