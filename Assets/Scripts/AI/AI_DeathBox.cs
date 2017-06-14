using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DeathBox : MonoBehaviour
{

    public Animator PrivoAnimator;
    public AudioSource Execution;
    public AudioSource ExecutionHuh;
    NPC npc;
    PlayerStats stats;
    bool PlayerInTrigger;
    [Header("Death")]
    [Tooltip("How much morality the player gets for killing the enemy\nShould be negative")]
    public int MoralityForKilling;
    public int MoralityForSaving;
    private bool Called = false;
    public GameObject Doll;
    public GameObject Ragdoll;
    private AI_Death AIDeath;

    void Start()
    {
        npc = GetComponentInParent<NPC>();
        AIDeath = GetComponentInParent<AI_Death>();
    }

    void Update()
    {
        if(PlayerInTrigger == true)
        {
            if (Input.GetKey(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5) && !Called)
            {
                Execution.PlayDelayed(0.5f);
                ExecutionHuh.PlayDelayed(0.5f);
                PrivoAnimator.SetTrigger("Execute");
                stats.Morality += MoralityForKilling;
                StartCoroutine("Death", 0.1f);
                Called = true;

                //Destroy(transform.parent.gameObject);

            }
            if (Input.GetKeyDown(KeyCode.JoystickButton4)&& !Called)
            {
                Debug.Log("1");
                stats.Morality += MoralityForSaving;
                AIDeath.mDeath = true;
                Called = true;
            }
        }
    }

    IEnumerator Death(float waitTime)
    {
        Debug.Log("a");
        npc.SetAnimation(NPCBase.AnimationState.Execute);
        yield return new WaitForSeconds(waitTime);
        //Ragdoll.SetActive(true);
        Instantiate(Ragdoll, transform.position + transform.forward, transform.rotation);
        //Doll.SetActive(false);
        Destroy(transform.parent.gameObject);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            stats = col.GetComponent<PlayerStats>();
            PlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            stats = col.GetComponent<PlayerStats>();
            PlayerInTrigger = false;
        }
    }
}
