using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Traps : MonoBehaviour
{
    GameObject WizardBase;
    Movement player;
    PlayerStats playerstats;
    WizardBoss Wizard;
    public ParticleSystem psTrap;
    bool playerEnter;
    float timer;
    [Tooltip("How long it will take before the trap picks a new direction")]
    public float newtargetTimer;
    [Tooltip("Speed of the trap")]
    public float speed;
    NavMeshAgent nav;
    Vector3 target;
    [Tooltip("How long it will take before the trap will do damage")]
    public float DamageWaitTime;
    bool canDamagePlayer;

    float waitTime;

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
            if(canDamagePlayer)
            {
                playerstats.ReceiveDamage(5 * Time.deltaTime);
            }
        }
    }
    void Awake()
    {
        //WizardBase = GameObject.Find("Wizard");
        WizardBase = GameObject.FindGameObjectWithTag("Wizard");
    }
    void Start()
    {

        Wizard = WizardBase.GetComponent<WizardBoss>();
        nav = gameObject.GetComponent<NavMeshAgent>();
        psTrap.Play();
        StartCoroutine("DestroyTraps");
        StartCoroutine("DelayDamage");
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
        timer += Time.deltaTime;
        if (timer >= newtargetTimer)
        {
            CalPath();
            timer = 0;
        }

    }
    void CalPath()
    {
        NewTarget();

        nav.speed = speed;
        nav.SetDestination(target);
    }
    void NewTarget()
    {

        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float xPos = myX + Random.Range(myX - 1, myX + 1);
        float zPos = myZ + Random.Range(myZ - 1, myZ + 1);

        NavMeshPath path = new NavMeshPath();
        nav.CalculatePath(target, path);

        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Path was not reachable");
            target = new Vector3(Wizard.transform.position.x, transform.position.y, Wizard.transform.position.z);
        }
        else
        {
            target = new Vector3(xPos, transform.position.y, zPos);
        }
    }

    IEnumerator DestroyTraps()
    {
        switch (Wizard.CurrentPhase)
        {
            case WizardBoss.Phase.Phase1:
                waitTime = Wizard.P1Timer;
                break;
            case WizardBoss.Phase.Phase2:
                waitTime = Wizard.P2Timer;
                break;
            case WizardBoss.Phase.Phase3:
                waitTime = Wizard.P3Timer;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
        Wizard.spawnerdone = false;
    }

    IEnumerator DelayDamage()
    {
        canDamagePlayer = false;
        yield return new WaitForSeconds(DamageWaitTime);
        canDamagePlayer = true;
    }
}
