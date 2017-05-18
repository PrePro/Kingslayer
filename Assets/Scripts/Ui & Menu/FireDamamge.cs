using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamamge : MonoBehaviour
{
    public float damage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerStats>().ReceiveDamage(damage);
        }
    }
}
