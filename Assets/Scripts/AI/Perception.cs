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
    GameObject PlayerHead;
    Ray ray;
    NPStats stats;
    // Use this for initialization
    void Start()
    {
        stats = GetComponentInParent<NPStats>();
        npc = GetComponentInParent<NPCBase>();
        sphereCollider = GetComponent<SphereCollider>();
        PlayerHead = GameObject.FindWithTag("PlayerHead");
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
            else
            {
                if (movement.isCrouching)
                {
                    Debug.Log("Crouch");
                    PlayerHead.transform.position = new Vector3(PlayerHead.transform.position.x, transform.position.y - 1.5f, PlayerHead.transform.position.z);
                }
                else
                {
                    PlayerHead.transform.position = new Vector3(PlayerHead.transform.position.x, transform.position.y, PlayerHead.transform.position.z);
                }
            }
        }
        else
        {
            Debug.Log("Perception is turned off");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerHead")
        {
            //PlayerHead.transform.position = other.gameObject.transform.position;
            //transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            direction = (other.gameObject.transform.position - transform.position).normalized;
            ray = new Ray(transform.position + direction, direction);
            if (Vector3.Dot(direction, transform.forward) >= 0)
            {
                Debug.Log("Target is in front of this game object.");
            }
            else
            {
                //Debug.Log("behind");
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
