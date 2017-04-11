using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAOE : MonoBehaviour
{
    public int Push;
    WizardBoss Wizard;
    public int AOEDamage;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerStats>().ReceiveDamage(AOEDamage);
            Vector3 dir = (transform.position - col.transform.position).normalized;
            col.transform.position -= dir * Push;
            gameObject.SetActive(false);
            Wizard.mHitCounter = 0;
        }
    }
    void Start()
    {
        Wizard = transform.parent.gameObject.GetComponent<WizardBoss>();
    }
}
