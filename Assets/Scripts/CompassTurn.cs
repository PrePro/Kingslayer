using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassTurn : MonoBehaviour
{
    bool isFacing = false;
    public GameObject Objective;
    public GameObject MiniMap;
    public GameObject player;
    public Transform arrow;
    public int Speed;

    Vector3 view;

    void Start()
    {

        //transform.rotation = player.transform.rotation;
        //  view = player.transform.position - Objective.transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        PositionArrow();
        //view = player.transform.position - Objective.transform.position;
        //transform.rotation = Quaternion.LookRotation(view);
        //Debug.DrawLine(player.transform.position, Objective.transform.position, Color.yellow);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, view.z);
        //transform.forward = direction;
    }


    void PositionArrow()
    {
        Vector3 dir = player.transform.InverseTransformPoint(Objective.transform.position);
        float a = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        a += 180;
        Debug.Log(a);
        arrow.localEulerAngles = new Vector3(0, 0, a);
            //transform.RotateAround(MiniMap.transform.position, Vector3.forward, Speed * Time.deltaTime);

    }

}
