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
    // Use this for initialization
    void Start()
    {
        npc = GetComponentInParent<NPCBase>();
        sphereCollider = GetComponent<SphereCollider>();
        PlayerHead = GameObject.FindWithTag("PlayerHead");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 15;
        Debug.DrawRay(transform.position + direction, ray.direction * 15, Color.red);
        if(movement == null)
        {
            return;
        }
        else
        {
            if (movement.isCrouching)
            {
                Debug.Log("Crouch");
                PlayerHead.transform.position = new Vector3(PlayerHead.transform.position.x, 0 , PlayerHead.transform.position.z);
            }
            else
            {
                PlayerHead.transform.position = new Vector3(PlayerHead.transform.position.x, 3, PlayerHead.transform.position.z);
            }
        }
       
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerHead")
        {
            //PlayerHead.transform.position = other.gameObject.transform.position;
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            direction = (other.gameObject.transform.position - transform.position).normalized;
            ray = new Ray(transform.position + direction, direction);

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
