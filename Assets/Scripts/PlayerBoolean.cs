using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoolean : MonoBehaviour
{

    public bool a;
    void Awake()
    {
        a = ToBaseTesting.TurnOn;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(a);
    }
}
