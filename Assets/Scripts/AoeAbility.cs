using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAbility : MonoBehaviour
{
    public int Push;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Vector3 dir = (transform.position - col.transform.position).normalized;
            col.transform.position -= dir * Push;

        }
    }
}
