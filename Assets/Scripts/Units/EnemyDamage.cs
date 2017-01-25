using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter(Collider col)
    {
        
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerStats>().ReceiveDamage(damage);
        }
    }
}
