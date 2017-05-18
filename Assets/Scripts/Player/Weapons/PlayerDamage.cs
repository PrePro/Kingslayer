using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    public int damage;
    public CoolDownSystem cdsystem;
    private bool mRunning = false;
    private NPStats stats;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (mRunning) return;

            mRunning = true;
            Debug.Log("Player hit enemy trigger");
            if (cdsystem.swing == true)
            {

                stats = col.GetComponent<NPStats>();
                if(stats == null)
                {
                    Debug.Log("Stats == null");
                    stats = col.GetComponentInParent<NPStats>();
                }
                if(stats != null)
                {
                    stats.ReceiveDamage(damage);
                }
            }
        }
        if (col.tag == "Wizard")
        {
            col.GetComponent<WizardBoss>().ReceiveDamage(damage);
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