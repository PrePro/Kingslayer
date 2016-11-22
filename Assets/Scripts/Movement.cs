using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    public CoolDownSystem coolDownSystem;
    public Transform target;
    public float speed;
    public float runningSpeed;
    float currentSpeed;
    public GameObject GameCamera;
    public Transform objectForward;
    bool isRuning = false;
    private float relativePosx;
    private float relativePosz;

    public float dashSpeedForward;
    public float dashSpeedLeft;
    public float dashSpeedRight;


    RaycastHit hit;
    void Start()
    {
        currentSpeed = speed;
        target.position = transform.position;
    }


    void Update()
    {
        Dashes();
        PlayerMove();
    }

    void Dashes()
    {
        if (coolDownSystem.CurrentDashState == CoolDownSystem.DashState.LeftDash)
        {
            transform.Translate((Vector3.left * Time.deltaTime * dashSpeedLeft));
            target.transform.position = objectForward.transform.position;
        }

        if (coolDownSystem.CurrentDashState == CoolDownSystem.DashState.RightDash)
        {
            transform.Translate((Vector3.right * Time.deltaTime * dashSpeedRight));
            target.transform.position = objectForward.transform.position;
        }

        if (coolDownSystem.CurrentDashState == CoolDownSystem.DashState.ForwardDash)
        {
            Debug.DrawRay(transform.position, Vector3.forward, Color.red);
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                Debug.Log(hit.collider.name);
            }
            transform.Translate((Vector3.forward * Time.deltaTime * dashSpeedForward));
            target.transform.position = objectForward.transform.position;
        }
    }


    void PlayerMove()
    {
        // Math
        relativePosx = target.position.x - transform.position.x;
        relativePosz = target.position.z - transform.position.z;

        //Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Look at the target objects postion if its 0.5 away 
        if (coolDownSystem.CurrentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (x >= 0.5f || x <= -0.5f || z >= 0.5f || z <= -0.5f)
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
        }

        //Player stops running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRuning = false;
        }
        if (coolDownSystem.CurrentDashState == CoolDownSystem.DashState.NotDashing)
        {
            //Player wants to move make them move 
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.Translate((Vector3.forward * Time.deltaTime * currentSpeed));
            }
        }
        //Target rotates around camera
        target.transform.rotation = GameCamera.transform.rotation;
        target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);

        // Moves the targets 
        if (coolDownSystem.CurrentDashState == CoolDownSystem.DashState.NotDashing)
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

}