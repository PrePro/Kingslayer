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

    public override void Run()
    {
        Debug.Log("Run");
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Exit()
    {
        Debug.Log("Exit");
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
