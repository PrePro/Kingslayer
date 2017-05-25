using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    private NPStats stats;
    private bool canAttack = true;
    [Tooltip("How much damage does the enemy do")]
    public int damage;
    [Tooltip("How Long the Enemy will wait to do damage again")]
    public float parryTimer;
    private bool isParry;
    public bool gotParry;

    void Start()
    {
        stats = GetComponentInParent<NPStats>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (canAttack == true && stats.Death == false)
            {
                if (isParry == false)
                col.GetComponent<PlayerStats>().ReceiveDamage(damage);
                StartCoroutine("damageTime", 1f);
            }
        }

        if (col.tag == "Parry")
        {
            StartCoroutine(ParryTimer(parryTimer));
            gotParry = true;
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
