using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Damage : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerStats>().ReceiveDamage(damage);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
