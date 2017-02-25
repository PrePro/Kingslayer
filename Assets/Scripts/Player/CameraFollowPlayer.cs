//======================================================================================================
// CameraFollowPlayer.cs
// Description:
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float targetAngle = 0;
    public float rotationDegree;
    private const float rotationAmount = 1.5f; // Dont touch
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetAngle -= rotationDegree;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngle += rotationDegree;
        }

        transform.position = player.transform.position + offset;

        if (targetAngle > 0)
        {
            transform.RotateAround(player.transform.position, Vector3.up, -rotationAmount);
            offset = transform.position - player.transform.position;

            targetAngle -= rotationAmount;
        }

        else if (targetAngle < 0)
        {
            transform.RotateAround(player.transform.position, Vector3.up, rotationAmount);
            offset = transform.position - player.transform.position;

            targetAngle += rotationAmount;
        }
    }
}
