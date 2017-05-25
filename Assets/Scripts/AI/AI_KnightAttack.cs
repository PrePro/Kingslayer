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
        if (npc.animator.GetCurrentAnimatorStateInfo(0).IsName("sword_and_shield_slash"))
        {
            Debug.Log("ATTACKING");
            agent.Stop();
            canTurn = false;
        }
        else
        {
            canTurn = true;
        }
        timer += Time.deltaTime;
        KAttackTarget();
    }

    public override void Enter()
    {
        npc.SetAnimation(NPCBase.AnimationState.Attacking);
        agent.Stop();
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
                if(canTurn)
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
            if(damage.gotParry)
            {
                //Debug.Log("GOT ANIM");
                hitspark.Play();
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
                npc.SetAnimation(NPCBase.AnimationState.Idle);
            }

        }
    }
}
