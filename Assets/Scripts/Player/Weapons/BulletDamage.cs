using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{ 
    public int damage;
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("Damage");
            col.GetComponent<NPStats>().ReceiveDamage(damage);
        }
    }
}
