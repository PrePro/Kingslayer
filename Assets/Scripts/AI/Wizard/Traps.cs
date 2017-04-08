using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Traps : MonoBehaviour
{
    public GameObject WizardBase;
    Movement player;
    PlayerStats playerstats;
    private WizardBoss Wizard;
    bool playerEnter;
    float timer;
    public float newtargetTimer;
    public float speed;
    NavMeshAgent nav;
    Vector3 target;
    int waitTime;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (playerEnter)
            {
                return;
            }


            player = col.GetComponent<Movement>();
            playerstats = col.GetComponent<PlayerStats>();
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

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            playerstats.ReceiveDamage(5 * Time.deltaTime);
        }
    }
    void Start()
    {
        WizardBase = GameObject.Find("Wizard");
        Wizard = WizardBase.GetComponent<WizardBoss>();
        nav = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine("DestroyTraps");
    }
    void Update()
    {
        if (playerEnter)
        {
            switch (Wizard.CurrentPhase)
            {
                case WizardBoss.Phase.Phase1:
                    player.currentSpeed = 3;
                    break;
                case WizardBoss.Phase.Phase2:

                    player.currentSpeed = 2;
                    break;
                case WizardBoss.Phase.Phase3:
                    player.currentSpeed = 0;
                    break;
                default:
                    break;
            }
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

    IEnumerator DestroyTraps()
    {
        switch (Wizard.CurrentPhase)
        {
            case WizardBoss.Phase.Phase1:
                waitTime = 2;
                break;
            case WizardBoss.Phase.Phase2:
                waitTime = 4;
                break;
            case WizardBoss.Phase.Phase3:
                waitTime = 8;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
        Wizard.spawnerdone = false;
    }
}
