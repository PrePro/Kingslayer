using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamamge : MonoBehaviour
{
    public int damage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerStats>().ReceiveDamage(damage);
        }
    }
}
