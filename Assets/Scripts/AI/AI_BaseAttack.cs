using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_BaseAttack : MonoBehaviour
{
    public abstract void Run();
    public abstract void Enter();
    public abstract void Exit();


    protected UnityEngine.AI.NavMeshAgent agent;
    protected NPStats stats;
    protected NPC npc;

    void Start()
    {
        npc = GetComponent<NPC>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stats = GetComponent<NPStats>();
    }

}