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
    Vector3 direction;
    Movement movement;
    Ray ray;
    NPStats stats;
    bool canSee;
    bool HasSeen;
    CoolDownSystem cd;

    // Use this for initialization
    void Start()
    {
        stats = GetComponentInParent<NPStats>();
        npc = GetComponentInParent<NPCBase>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.Death == false)
        {
            //Debug.Log(" Npc is Dead");
            Debug.DrawRay(transform.position + direction, ray.direction * 15, Color.red);
            if (movement == null)
            {
                return;
            }
        }
        else
        {
            //Debug.Log("Perception is turned off");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerHead")
        {
            //PlayerHead.transform.position = other.gameObject.transform.position;
            //transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            //Debug.Log("Target is in front of this game object.");
            if (npc.dominantBehavior == NPCBase.Behavior.IdleDefencive || npc.dominantBehavior == NPCBase.Behavior.PatrolDefencive)
            {
                //CoolDownSystem cd = other.GetComponent<CoolDownSystem>();
                if (cd.currentAnimState == CoolDownSystem.PlayerState.SwordInHand)
                {
                    HasSeen = true;
                }
                else
                {
                    if(HasSeen)
                    {
                        Debug.Log("Has Seen");
                    }
                    else
                    {
                        return;
                    }
                }
            }
            

            direction = (other.gameObject.transform.position - transform.position).normalized;
            ray = new Ray(transform.position + direction, direction);
            canSee = Vector3.Dot(direction, transform.forward) >= 0;
            if (canSee)
            {
                //Debug.Log("Target is in front of this game object.");
            }
            else
            {
                if(stats.tookDamage)
                {
                    npc.OnTargetFound(other.gameObject);
                    //Debug.Log("Took damage from behind");
                }
                //Debug.Log("Target is in behind of this game object.");
                return;
            }
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (npc.dominantBehavior == NPCBase.Behavior.IdleDefencive || npc.dominantBehavior == NPCBase.Behavior.PatrolDefencive)
            {
                cd = other.GetComponent<CoolDownSystem>();
            }
                movement = other.GetComponent<Movement>();
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
