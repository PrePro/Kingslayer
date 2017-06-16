using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAOE : MonoBehaviour
{
    public int Push;
    public int AOEDamage;
    public float ExpandAmount;
    private bool Expand = false;
    private bool mayBe = false;
    public GameObject player;
    public Movement movement;
    public Animation anim;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            mayBe = false;
            col.GetComponent<Animator>().SetBool("privoKnockBack", true);
            col.GetComponent<PlayerStats>().ReceiveDamage(AOEDamage);
            Vector3 dir = (transform.position - col.transform.position).normalized;
            col.transform.position -= dir * Push;
            player.GetComponent<Movement>().enabled = false;
            StartCoroutine("knockUp", col);
        }
    }
    void Start()
    {
    }

    void Update()
    {
        if(Expand)
        {
            transform.localScale += new Vector3(ExpandAmount, ExpandAmount, ExpandAmount);
            StartCoroutine("ExpandTime", 4f);
        }
        if(mayBe == true)
        {
            Debug.Log("upInhere");
            player.GetComponent<Animator>().SetBool("privoKnockBack", false);
            StartCoroutine("startWalk");
        }
    }

    public IEnumerator ExpandTime(float waitTime)
    {
        //Expand = true;
        yield return new WaitForSeconds(waitTime);
        Expand = false;
        gameObject.SetActive(false);
        player.GetComponent<Movement>().enabled = true;

    }
    IEnumerator knockUp(Collider c)
    {
        yield return new WaitForSeconds(.1f);
        mayBe = true;
    }
    IEnumerator startWalk()
    {
        yield return new WaitForSeconds(2.9f);
        player.GetComponent<Movement>().enabled = true;
        gameObject.SetActive(false);
    }
}
