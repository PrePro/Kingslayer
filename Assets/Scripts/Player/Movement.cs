//======================================================================================================
// Movement.cs
// Description: Players Movement
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    //======================================================================================================
    // Variables
    //======================================================================================================
    #region Variables
    [Header("Game Objects")]
    [Tooltip("Game Objects to be used")]
    public Transform target;
    public CoolDownSystem coolDownSystem;
    public Transform objectForward;
    public GameObject gameCamera;

    [Header("Player Speeds")]
    [Tooltip("Speeds")]
    public float speed;
    public float runningSpeed;
    private float currentSpeed;

    private bool isRuning = false;
    private float relativePosx;
    private float relativePosz;

    [Header("Player Dash Speeds")]
    [Tooltip("How fast the player will dash")]
    public float dashSpeedForward;
    public float dashSpeedLeft;
    public float dashSpeedRight;

    [Header("Disable Movement")]
    [Tooltip("Disable movement and time it will stop")]
    public bool stopMovement = false;
    [SerializeField]
    private float StopTimer;
    [Header("Animation")]
    [Tooltip("...")]
    [SerializeField]
    private Animator myAnimator;
    public bool isWalking;
    public bool isRunning;
    #endregion

    //======================================================================================================
    // GameObject Functions
    //======================================================================================================
    #region GameObject Functions
    void Start()
    {
        isWalking = false;
        StartCoroutine("StopMovement", StopTimer);
        currentSpeed = speed;
        target.position = transform.position;
    }

    void Update()
    {
        if (stopMovement == false)
        {
            Dashes();
            PlayerMove();
        }
    }
    #endregion

    //======================================================================================================
    // Private Member Functions 
    //======================================================================================================
    #region Private Member Functions
    private void Dashes()
    {
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.LeftDash)
        {
            transform.Translate((Vector3.left * Time.deltaTime * dashSpeedLeft));
            target.transform.position = objectForward.transform.position;
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.RightDash)
        {
            transform.Translate((Vector3.right * Time.deltaTime * dashSpeedRight));
            target.transform.position = objectForward.transform.position;
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.ForwardDash)
        {
            transform.Translate((Vector3.forward * Time.deltaTime * dashSpeedForward));
            target.transform.position = objectForward.transform.position;
        }
    }
  

    private void PlayerMove()
    {
        // Math
        relativePosx = target.position.x - transform.position.x;
        relativePosz = target.position.z - transform.position.z;

        //Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Look at the target objects postion if its 0.4 away 
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (x >= 0.4f || x <= -0.4f || z >= 0.4f || z <= -0.4f)
            {
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePosx, 0, relativePosz));
                transform.rotation = rotation;
            }
        }


        //Cant walk in both directions
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            currentSpeed = 0;
        }

        //Cant walk in both directions
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            currentSpeed = 0;
        }
        // Starts Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRuning = true;
            myAnimator.SetBool("privoRun", isRuning);//Added for sprint animation. 
        }

        //Player stops running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRuning = false;
            myAnimator.SetBool("privoRun", isRuning);//Added for sprint animation. 
        }
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            //Player wants to move make them move 
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.Translate((Vector3.forward * Time.deltaTime * currentSpeed));
                isWalking = true;
                myAnimator.SetBool("privoWalk", isWalking);
            }
            else
            {
                isWalking = false;
                myAnimator.SetBool("privoWalk", isWalking);
            }
        }
        //Target rotates around camera
        target.transform.rotation = gameCamera.transform.rotation;
        target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);



        // Moves the targets 
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {

                if (isRuning == false)
                {
                    currentSpeed = speed;
                }
                else
                {
                    currentSpeed = runningSpeed;
                }

                target.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
            }

            // Moves the targets 
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                if (isRuning == false)
                {
                    currentSpeed = speed;
                }
                else
                {
                    currentSpeed = runningSpeed;
                }
                target.Translate(Vector3.right * Time.deltaTime * currentSpeed);

            }

            // Moves the targets 
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {

                if (isRuning == false)
                {
                    currentSpeed = speed;
                }
                else
                {
                    currentSpeed = runningSpeed;
                }
                target.Translate(Vector3.back * Time.deltaTime * currentSpeed);
            }

            // Moves the targets 
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {

                if (isRuning == false)
                {
                    currentSpeed = speed;
                }
                else
                {
                    currentSpeed = runningSpeed;
                }
                target.Translate(Vector3.left * Time.deltaTime * currentSpeed);
            }
        }

        // Reset
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            target.transform.position = objectForward.transform.position;

            if (isRuning == false)
            {
                currentSpeed = speed;
            }
            else
            {
                currentSpeed = runningSpeed;
            }

        }

    }

    IEnumerator StopMovement(float waitTime)
    {
        stopMovement = true;
        yield return new WaitForSeconds(waitTime);
        stopMovement = false;
    }
    #endregion

}