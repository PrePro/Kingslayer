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
    public int i = 0;

    Quaternion y;
    Quaternion y1;
    // Use this for initialization
    void Start()
    {
        offset = mCamera.transform.position - Player.Position;
        y = mCamera.transform.rotation;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (i == 0) //&& mCamera.transform.rotation.y == -106
            {
                targetAngle -= rotationDegree;
                i += 1;
            }
            else if(i == 1)
            {
                targetAngle += rotationDegree;
                i = 0;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(mCamera.transform.eulerAngles.y);
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
