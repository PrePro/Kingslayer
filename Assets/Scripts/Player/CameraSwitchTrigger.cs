using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public float targetAngle = 0;
    public float rotationDegree;
    private Vector3 offset;
    private const float rotationAmount = 1.5f; // Dont touch
    public GameObject mCamera;
    static public bool turn = false;

    int a;

    // Use this for initialization
    void Start()
    {
        offset = mCamera.transform.position - Player.Position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (turn == false) //&& mCamera.transform.rotation.y == -106
            {
                targetAngle -= rotationDegree;
                turn = true;
            }
            else if(turn == true)
            {
                targetAngle += rotationDegree;
                turn = false;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(turn + gameObject.name);
        //Debug.Log(mCamera.transform.eulerAngles.y);
        if (targetAngle > 90)
        {
            targetAngle = 90;
        }
        else if (targetAngle < -90)
        {
            targetAngle = -90;
        }

        if (targetAngle > 0)
        {
            mCamera.transform.RotateAround(Player.Position, Vector3.up, -rotationAmount);
            offset = mCamera.transform.position - Player.Position;

            //mCamera.transform.position = Player.Position + offset;
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            mCamera.transform.RotateAround(Player.Position, Vector3.up, rotationAmount);
            offset = mCamera.transform.position - Player.Position;

            //mCamera.transform.position = Player.Position + offset;
            targetAngle += rotationAmount;
        }
    }
}
