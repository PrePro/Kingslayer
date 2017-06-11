using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_KnightAttack : AI_BaseAttack
{
    EnemyDamage damage;
    public bool isFacing = false;
    public bool isInRange = false;
    public bool isReady = false;
    public bool canTurn = true;
    public ParticleSystem slash;
    public int TurnSpeed;
    int[] randomAnim1 = new int[] { 2, 9, 10, 11 };
    public AudioSource ParrySound;
    float timer;

    [SerializeField]
    [Tooltip("This really shouldnt be touched unless new Enemy")]
    public float attackRange;
    [Tooltip("How fast the enemy attacks")]
    [SerializeField]
    protected float attackSpeed;
    public bool MultiAnim;
    public ParticleSystem hitspark;

    void Start()
    {
        damage = GetComponentInChildren<EnemyDamage>();
        if (damage == null)
        {
            Debug.Log("NULL");
        }

    }

    public override void Run()
    {
        //int a = npc.animator.GetInteger("AnimationState");
        //if(a == 2)
        //{
        //    agent.isStopped = true;
        //    canTurn = false;
        //}
        //else if(a == 9)
        //{
        //    agent.isStopped = true;
        //    canTurn = false;
        //}
        //else if (a == 10)
        //{
        //    agent.isStopped = true;
        //    canTurn = false;
        //}
        //else if (a == 11)
        //{
        //    agent.isStopped = true;
        //    canTurn = false;
        //}
        //else
        //{
        //    canTurn = true;
        //}

        //if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("sword_and_shield_slash"))
        //{
        //    Debug.Log("ATTACKING");
        //    agent.isStopped = true;
        //    canTurn = false;
        //}
        //else
        //{
        //    canTurn = true;
        //}
        checkAnimation();
        timer += Time.deltaTime;
        KAttackTarget();
    }

    void checkAnimation()
    {
        if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("sword_and_shield_slash"))
        {
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_and_shield_slash 2"))
        {
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_and_shield_slash 3"))
        {
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Great_Sword_Slash"))
        {
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Great_Sword_Slash 2"))
        {
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Great_Sword_Slash 3"))
        {
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Low Spin Attack"))
        {
            Debug.Log("Low Spin Attack");
            agent.isStopped = true;
            canTurn = false;
        }
        else  if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Spin Slash"))
        {
            Debug.Log("Spin Slash");
            agent.isStopped = true;
            canTurn = false;
        }
        else if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Attack"))
        {
            Debug.Log("Jump Attack");
            agent.isStopped = true;
            canTurn = false;
        }
        else
        {
            Debug.Log("Else");
            canTurn = true;
        }
    }

    public override void Enter()
    {
        npc.SetAnimation(NPCBase.AnimationState.Attacking);
        agent.isStopped = true;
        slash.Play();
        npc.searchingImage.SetActive(false);
        npc.foundImage.SetActive(false);

    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }

    public void KAttackTarget()
    {
        if (stats.Death)
        {
            return;
        }

        isInRange = GameplayStatics.IsWithinRange2D(transform, npc.currentTarget.position, attackRange);

        isFacing = GameplayStatics.IsFacing(transform, npc.currentTarget.position);

        if (isFacing)
        {
            if (!isInRange)
            {
                Debug.Log("!not in range");
                isInRange = false;
                if (!npc.isTargetSeen)
                {
                    npc.SetState(NPCBase.State.Searching);
                }
                else
                {
                    npc.SetState(NPCBase.State.Chasing);
                }
            }
            else
            {
                isReady = true;
            }
        }
        else // Is not facing
        {
            if (isInRange)
            {
                if (canTurn)
                {
                    Vector3 target = npc.currentTarget.position;
                    target.y = transform.position.y;
                    target = target - transform.position;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, target, TurnSpeed * Time.deltaTime, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
            else
            {
                isInRange = false;
                if (!npc.isTargetSeen)
                {
                    npc.SetState(NPCBase.State.Searching);
                }
                else
                {
                    npc.SetState(NPCBase.State.Chasing);
                }
            }
        }

        if (timer >= attackSpeed && isReady)
        {
            if (MultiAnim)
            {
                int RandomAnimation = randomAnim1[UnityEngine.Random.Range(0, randomAnim1.Length)];
                npc.SetAnimation((NPCBase.AnimationState)RandomAnimation);
            }
            else
            {
                npc.SetAnimation(NPCBase.AnimationState.Attacking);
            }

            timer = 0;
            isReady = false;
        }
        else
        {
            //Debug.Log(damage.gotParry);
            if (damage.gotParry)
            {
                //Debug.Log("GOT ANIM");
                if(hitspark != null)
                {
                    ParrySound.PlayDelayed(0.1f);
                    if (hitspark.isPlaying != true)
                    {
                        hitspark.Play();
                    }
                }
                npc.SetAnimation(NPCBase.AnimationState.ParryStagger);
                damage.gotParry = false;
            }
            else if (stats.HitAoe)
            {
                //Debug.Log("Got hit aoe");
                npc.SetAnimation(NPCBase.AnimationState.AOEKnockBack);
                stats.HitAoe = false;
            }
            else
            {
                if(agent.isStopped)
                {
                    npc.SetAnimation(NPCBase.AnimationState.Idle);
                }
                else
                {
                    npc.SetAnimation(NPCBase.AnimationState.Walking);
                }
            }

        }
    }
}
