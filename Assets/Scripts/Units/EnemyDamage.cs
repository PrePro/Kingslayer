using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float parryTimer;
    private bool isParry;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if(isParry == false)
            col.GetComponent<PlayerStats>().ReceiveDamage(damage);
        }
        if(col.tag == "Parry")
        {
            Debug.Log("Parry");
            StartCoroutine(ParryTimer(parryTimer));
        }
    }

    IEnumerator ParryTimer(float waitTime)
    {
        isParry = true;
        yield return new WaitForSeconds(waitTime);
        isParry = false;
    }
}
