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
            Debug.Log("Player");
            col.GetComponent<PlayerStats>().ReceiveDamage(AOEDamage);
            col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z - Push);
            //Vector3 dir = (transform.position - col.transform.position);
            //Debug.Log(dir);
            //col.transform.position -= dir * Push;
            gameObject.SetActive(false);
            Wizard.mHitCounter = 0;
        }
    }
    void Start()
    {
        Wizard = transform.parent.gameObject.GetComponent<WizardBoss>();
    }
}
