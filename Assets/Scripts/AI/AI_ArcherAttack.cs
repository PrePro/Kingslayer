using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ArcherAttack : AI_BaseAttack
{
    [Header("Archer")]
    [Tooltip("bullet Object to be fired")]
    [SerializeField]
    protected GameObject Bullet;
    [Tooltip("Where the bulllet is shot from")]
    [SerializeField]
    protected GameObject BulletTarget;
    [Tooltip("How fast the archer attacks")]
    [SerializeField]
    protected float bulletSpeed;

    public bool isFacing;
    public int TurnSpeed;

    [Tooltip("How fast the enemy attacks")]
    [SerializeField]
    protected float attackSpeed;

    float timer;

    public override void Run()
    {
        Debug.Log("Run");

        timer += Time.deltaTime;
        AAttackTarget();
    }

    public override void Enter()
    {
        Debug.Log("Enter");
        timer = attackSpeed;
        npc.SetAnimation(NPCBase.AnimationState.Attacking);
        agent.Stop();
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }

    public void AAttackTarget()
    {
        if (stats.Death)
        {
            return;
        }

        if (timer >= attackSpeed)
        {
            Shoot();
            timer = 0;
        }

        //isInRange = GameplayStatics.IsWithinRange2D(transform, npc.currentTarget.position, attackRange);
        isFacing = GameplayStatics.IsFacing(transform, npc.currentTarget.position);

        if (!isFacing)
        {
            Vector3 target = npc.currentTarget.position;
            target.y = transform.position.y;
            target = target - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, target, TurnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        //if (isFacing)
        //{
        //    if (!isInRange)
        //    {
        //        Debug.Log("!not in range");
        //        isInRange = false;
        //        if (!npc.isTargetSeen)
        //        {
        //            npc.SetState(NPCBase.State.Searching);
        //        }
        //        else
        //        {
        //            npc.SetState(NPCBase.State.Chasing);
        //        }
        //    }
        //    else
        //    {
        //        isReady = true;
        //    }
        //}
        //else // Is not facing
        //{
        //    if (isInRange)
        //    {
        //        if (canTurn)
        //        {
        //            Vector3 target = npc.currentTarget.position;
        //            target.y = transform.position.y;
        //            target = target - transform.position;
        //            Vector3 newDir = Vector3.RotateTowards(transform.forward, target, TurnSpeed * Time.deltaTime, 0.0f);
        //            transform.rotation = Quaternion.LookRotation(newDir);
        //        }
        //    }
        //    else
        //    {
        //        isInRange = false;
        //        if (!npc.isTargetSeen)
        //        {
        //            npc.SetState(NPCBase.State.Searching);
        //        }
        //        else
        //        {
        //            npc.SetState(NPCBase.State.Chasing);
        //        }
        //    }
        //}

        //if (timer >= attackSpeed && isReady)
        //{
        //        npc.SetAnimation(NPCBase.AnimationState.Attacking);
        //    Shoot();
        //    timer = 0;
        //    isReady = false;
        //}
        //else
        //{
        //    npc.SetAnimation(NPCBase.AnimationState.Idle);

        //}
    }

    private void Shoot()
    {
        Vector3 firePosition = BulletTarget.transform.position;
        GameObject bullet = GameObject.Instantiate(Bullet, firePosition, BulletTarget.transform.rotation) as GameObject;

        if (bullet != null)
        {
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * bulletSpeed;
            rigidbody.AddForce(force);
        }
    }

}
