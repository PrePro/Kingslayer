using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public Movement movement;

    void Start()
    {

    }

    void Update()
    {
        movement.stopMovement = false;
    }
}
