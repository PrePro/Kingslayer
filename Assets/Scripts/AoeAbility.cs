using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAbility : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            
            col.transform.position -= new Vector3 (2 , 2, 2);
        }
    }
}
