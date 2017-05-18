using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassTurn : MonoBehaviour
{
    bool isFacing = false;
    public GameObject Objective;
    public GameObject MiniMap;
    public GameObject player;
    public int Speed;
    public float distance;
    public float height;
    public float damping;
    public float rotDamp;

    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        //PositionArrow();
    }


    //void PositionArrow()
    //{
    //    isFacing = GameplayStatics.IsFacing(transform, Objective.transform.position);
    //    if (isFacing)
    //    {
    //        Debug.Log("ISFACING");
    //    }
    //    transform.rotation.eulerAngles.y = Objective.transform.rotation.eulerAngles.y;
    //    //transform.RotateAround(MiniMap.transform.position, Vector3.forward, Speed * Time.deltaTime);

    //}

}
