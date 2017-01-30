using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    private bool canAttack = true;
    public int damage;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log(canAttack);
            if (canAttack == true)
            { 
                col.GetComponent<PlayerStats>().ReceiveDamage(damage);
                StartCoroutine("damageTime", 1f);
            }
        }
    }

    IEnumerator damageTime(float waitTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }
}
