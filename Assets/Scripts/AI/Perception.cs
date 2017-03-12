using UnityEngine;
using System.Collections;

public class Perception : MonoBehaviour
{

    NPCBase npc;
    [SerializeField]
    LayerMask targetLayer;
    [SerializeField]
    LayerMask obstructionLayer;
    SphereCollider sphereCollider;
    // Use this for initialization
    void Start()
    {
        npc = GetComponentInParent<NPCBase>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var direction = (other.gameObject.transform.position - transform.position).normalized;
            Ray ray = new Ray(transform.position + direction, direction);
            var hits = Physics.RaycastAll(ray, sphereCollider.radius, obstructionLayer);

            float targetDistance = Vector3.Distance(transform.position, other.transform.position);
            float barrierDistance = float.MaxValue;
            bool hitBarrier = hits.Length > 0;

            foreach (var hit in hits)
            {
                if (barrierDistance > hit.distance)
                {
                    barrierDistance = hit.distance;
                }
            }
            if (!hitBarrier)
            {
                npc.OnTargetFound(other.gameObject);
            }
            else if (hitBarrier && barrierDistance > targetDistance)
            {
                npc.OnTargetFound(other.gameObject);
            }
            else
            {
                npc.OnTargetLost();
            }
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
