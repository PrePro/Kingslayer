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

    public int PhaseOneDamage;
    public int PhaseTwoDamage;
    public int PhaseThreeDamage;


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
                switch (Wizard.CurrentPhase)
                {
                    case WizardBoss.Phase.Phase1:
                        playerstats.ReceiveDamage(PhaseOneDamage * Time.deltaTime);
                        break;
                    case WizardBoss.Phase.Phase2:
                        playerstats.ReceiveDamage(PhaseTwoDamage * Time.deltaTime);
                        break;
                    case WizardBoss.Phase.Phase3:
                        playerstats.ReceiveDamage(PhaseThreeDamage * Time.deltaTime);
                        break;
                    case WizardBoss.Phase.Killed:
                        playerstats.ReceiveDamage(0);
                        break;
                    default:
                        break;
                }
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

        float minX = gameObject.transform.position.x;
        float minZ = gameObject.transform.position.z;

        minX += (Random.Range(-20, 20));
        minZ += (Random.Range(-20, 20));

        NavMeshPath path = new NavMeshPath();
        nav.CalculatePath(target, path);

        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Path was not reachable");
            target = new Vector3(Wizard.transform.position.x, transform.position.y, Wizard.transform.position.z);
        }
        else
        {
            target = new Vector3(minX, transform.position.y, minZ);
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
