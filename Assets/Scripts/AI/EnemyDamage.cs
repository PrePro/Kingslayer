using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    private NPStats stats;
    private NPC npc;
    private bool canAttack = true;
    private bool AttackState = false;
    [Tooltip("How much damage does the enemy do")]
    public int damage;
    [Tooltip("How Long the Enemy will wait to do damage again")]
    public float parryTimer;
    private bool isParry;
    public bool gotParry;

    void Start()
    {
        stats = GetComponentInParent<NPStats>();
        npc = GetComponentInParent<NPC>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if(stats.Death == true)
            {
                Debug.Log("ASd");
                return;
            }
           
            if (canAttack && AttackState)
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

    void Update()
    {
        if (npc.CurrentState == NPCBase.State.Attacking)
        {
            AttackState = true;
        }
        else
        {
            AttackState = false;
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
