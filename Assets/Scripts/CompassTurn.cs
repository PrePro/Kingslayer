using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassTurn : MonoBehaviour
{
    public GameObject[] Objective;
    public GameObject player;
    public Transform arrow;
    private int i;

    void Start()
    {
    }

    public void GotoNextObjective()
    {
        if(i >= Objective.Length)
        {
            return;
        }

        i++;
    }

    // Update is called once per frame
    void Update()
    {

        PositionArrow();
        //Debug.Log(Objective[0].name);
    }


    void PositionArrow()
    {
        //Vector3 dir = player.transform.InverseTransformPoint(Objective.transform.position);
        Vector3 dir = (Objective[i].transform.position - player.transform.position);
        //Debug.DrawLine(player.transform.position, dir, Color.red);
        float a = Mathf.Atan2(dir.x, -dir.z) * Mathf.Rad2Deg;
        a += 180;
        arrow.localEulerAngles = new Vector3(0, 0, a);

            //transform.RotateAround(MiniMap.transform.position, Vector3.forward, Speed * Time.deltaTime);

    }



}
