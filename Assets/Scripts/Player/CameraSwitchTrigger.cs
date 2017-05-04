using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    private float targetAngle = 0;
    public float rotationDegree;
    public float rotationDegree2;
    private Vector3 offset;
    private const float rotationAmount = 1.5f; // Dont touch
    public GameObject camera;
    public GameObject player;
    private int i;
    // Use this for initialization
    void Start()
    {
        offset = camera.transform.position - Player.Position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            player = col.gameObject;
            i += 1;
            if (i == 0)
            {
                targetAngle -= rotationDegree;
            }
            else
            {
                targetAngle -= rotationDegree2;
                i = 0;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {

        if (targetAngle > 0)
        {
            Debug.Log("a");
            camera.transform.RotateAround(Player.Position, Vector3.up, -rotationAmount);
            offset = transform.position - Player.Position;

            targetAngle -= rotationAmount;
        }

        else if (targetAngle < 0)
        {
            //camera.transform.position = Player.Position + offset;
            Debug.Log("b");
            camera.transform.RotateAround(Player.Position, Vector3.up, rotationAmount);
            offset = camera.transform.position - Player.Position;

            targetAngle += rotationAmount;
        }
    }
}
