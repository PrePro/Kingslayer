using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    public int damage;
    public CoolDownSystem cdsystem;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //if(cdsystem.swing == true)
           // {
                Debug.Log("Damage");
                col.GetComponent<NPStats>().ReceiveDamage(damage);
           //}
        }
    }

}
