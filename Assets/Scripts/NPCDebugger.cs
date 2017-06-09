using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDebugger : MonoBehaviour
{
    private NPC npc;
    public Text Class;
    public Text State;
    public Text DominantBehavior;

    void Awake()
    {
        npc = GetComponentInParent<NPC>();
    }


    void Update()
    {
        Class.text = "" + npc.unitClass;
        State.text = "" + npc.CurrentState;
        DominantBehavior.text = "" + npc.dominantBehavior;
    }
}
