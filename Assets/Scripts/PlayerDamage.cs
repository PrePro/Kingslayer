using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    public int damage;
    public CoolDownSystem cdsystem;
    private bool mRunning = false;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            if (mRunning) return;

            mRunning = true;
            Debug.Log("Player hit enemy trigger");
           // if(cdsystem.swing == true)
            //{
                col.GetComponent<NPStats>().ReceiveDamage(damage);
           // }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            mRunning = false;
        }

    }
}
