using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Base : MonoBehaviour
{

    protected UnityEngine.AI.NavMeshAgent agent;
    public abstract float CalValue();
    public abstract void Run();
    public abstract void Enter();
    public abstract void Exit();

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
}
