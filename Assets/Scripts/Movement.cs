using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public PlayerDamage Pdamage;
    public Transform target;
    public float speed;
    public float runningSpeed;
    float currentSpeed;
    public GameObject GameCamera;
    public Transform objectForward;
    bool isRuning = false;
    private float relativePosx;
    private float relativePosz;

    void Start()
    {
        currentSpeed = speed;
        target.position = transform.position;
    }


    void Update()
    {
        PlayerMove();
        RayCast();
    }

    void RayCast()
    {
        Vector3 direction = (GameCamera.transform.position - transform.position).normalized;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        Debug.DrawRay(transform.position, direction,
           Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                //Color tempColor = hit.collider.GetComponent<Renderer>().material.color;
                //hit.collider.GetComponent<Renderer>().material.color = new Color(tempColor.r, tempColor.g, tempColor.b, 50f);
                //Debug.Log("Alpha Channel " + tempColor.a);
                Destroy(hit.collider.gameObject);
            }
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
        if (Pdamage.isDashing == false)
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
        if (Pdamage.isDashing == false)
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
        if (Pdamage.isDashing == false)
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