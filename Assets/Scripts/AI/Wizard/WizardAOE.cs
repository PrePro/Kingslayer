using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAOE : MonoBehaviour
{
    public int Push;
    WizardBoss Wizard;
    public int AOEDamage;
    public float ExpandAmount;
    private bool Expand = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerStats>().ReceiveDamage(AOEDamage);
            Vector3 dir = (transform.position - col.transform.position).normalized;
            col.transform.position -= dir * Push;
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        Wizard = transform.parent.gameObject.GetComponent<WizardBoss>();
    }

    void Update()
    {
        if(Expand)
        {
            transform.localScale += new Vector3(ExpandAmount, ExpandAmount, ExpandAmount);
        }
    }

    public IEnumerator ExpandTime(float waitTime)
    {
        Expand = true;
        yield return new WaitForSeconds(waitTime);
        Expand = false;
        gameObject.SetActive(false);
    }
}
