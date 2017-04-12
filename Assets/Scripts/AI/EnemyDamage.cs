using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    private bool canAttack = true;
    [Tooltip("How much damage does the enemy do")]
    public int damage;
    [Tooltip("How Long the Enemy will wait to do damage again")]
    public float parryTimer;
    private bool isParry;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (canAttack == true)
            {
                if (isParry == false)
                col.GetComponent<PlayerStats>().ReceiveDamage(damage);
                StartCoroutine("damageTime", 1f);
            }
        }

        if (col.tag == "Parry")
        {
            Debug.Log("Parry");
            StartCoroutine(ParryTimer(parryTimer));
        }
    }

    IEnumerator damageTime(float waitTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }

    IEnumerator ParryTimer(float waitTime)
    {
        isParry = true;
        yield return new WaitForSeconds(waitTime);
        isParry = false;
    }
}
