using UnityEngine;
using System.Collections;

public class Perception : MonoBehaviour
{
    NPCBase npc;
    [SerializeField]
    LayerMask targetLayer;
    // Use this for initialization
    void Start()
    {
        npc = GetComponentInParent<NPCBase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Ray ray = new Ray(transform.position, (other.gameObject.transform.position - transform.position).normalized);
            if (!Physics.Raycast(ray, 15.0f, ~targetLayer))
            {
                npc.OnTargetFound(other.gameObject);
            }
            else
            {
                npc.OnTargetLost();
            }
        }
        else
        {
            npc.OnTargetLost();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            npc.OnTargetLost();
        }
    }
}
