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
    BoxCollider collider;
    static public bool turn = false;


    // Use this for initialization
    void Awake()
    {
        collider = GetComponent<BoxCollider>();
        offset = mCamera.transform.position - Player.Position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "CameraSwitch")
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
            if(collider.gameObject.activeSelf)
            {
                collider.isTrigger = false;
            }
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "CameraSwitch")
        {
            collider.gameObject.SetActive(true);
        }
    }


    void Update()
    {
        Debug.Log("ASDAS");
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
