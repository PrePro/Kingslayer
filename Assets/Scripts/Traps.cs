using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Traps : MonoBehaviour
{
    public GameObject WizardBase;
    private WizardBoss Wizard;
    bool playerEnter;
    float timer;
    public float newtargetTimer;
    public float speed;
    NavMeshAgent nav;
    Vector3 target;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (playerEnter)
            {
                return;
            }

            playerEnter = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            if (playerEnter)
            {
                playerEnter = false;
            }
        }
    }
    void Start()
    {
        WizardBase = GameObject.Find("Wizard");
        Wizard = WizardBase.GetComponent<WizardBoss>();
        nav = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine("DestroyTraps", 5);
    }

    void Update()
    {
        if (playerEnter)
        {
            Debug.Log("ENTER");
        }
        if (Wizard.CurrentPhase != WizardBoss.Phase.Phase1)
        {
            timer += Time.deltaTime;
            if (timer >= newtargetTimer)
            {
                NewTarget();
                timer = 0;
            }
        }
        if (Wizard.CurrentPhase != WizardBoss.Phase.Phase2)
        {

        }

    }

    void NewTarget()
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float xPos = myX + Random.Range(myX - 10, myX + 10);
        float zPos = myZ + Random.Range(myZ - 10, myZ + 10);

        target = new Vector3(xPos, transform.position.y, zPos);

        nav.speed = speed;
        nav.SetDestination(target);
    }

    IEnumerator DestroyTraps(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
        Wizard.spawnerdone = false;
    }
}
