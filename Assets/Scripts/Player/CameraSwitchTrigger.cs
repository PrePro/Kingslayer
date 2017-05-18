using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public float targetAngle = 0;
    public float rotationDegree;
    static private Vector3 offset;
    private const float rotationAmount = 1.5f; // Dont touch
    public GameObject mCamera;
    static public bool turn = false;


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
            else if (turn == true)
            {
                targetAngle += rotationDegree;
                turn = false;
            }

        }
    }


    void Update()
    {
        if (targetAngle > 90)
        {
            targetAngle = 90;
        }
        else if (targetAngle < -90)
        {
            targetAngle = -90;
        }

        mCamera.transform.position = Player.Position + offset;



        if (targetAngle > 0)
        {
            mCamera.transform.RotateAround(Player.Position, Vector3.up, -rotationAmount);
            offset = mCamera.transform.position - Player.Position;

            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            mCamera.transform.RotateAround(Player.Position, Vector3.up, rotationAmount);
            offset = mCamera.transform.position - Player.Position;

            targetAngle += rotationAmount;
        }
    }
}
