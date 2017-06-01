using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerception : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    public List<GameObject> list;
    private Movement movement;
    [SerializeField]
    public bool LookAtEnemy;
    private float timer;
    public float CoolDown;
    public int index;

    void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    void Update()
    {
        if(CoolDown + 1 > timer)
        {
            timer += Time.deltaTime;
        }

        if (list.Capacity != 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if(index + 1 < list.Count)
                {
                    index += 1;
                }
            
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                if (index - 1 >= 0)
                {
                    index -= 1;
                }
            }
        }

        if (timer >= CoolDown)
        {
            if (Input.GetKeyDown(KeyCode.X) && list.Count != 0)
            {
                StartCoroutine(lookatenemy(0.1f));
                timer = 0;
            }

        }

        if (LookAtEnemy)
        {
            Player.transform.position = list[index].transform.position;

        }


    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (!list.Contains(col.gameObject))
            {
                list.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (list.Contains(col.gameObject))
            {
                list.Remove(col.gameObject);
                if(index > list.Count)
                {
                    index -= 1;
                }
            }
        }
    }

    public IEnumerator lookatenemy(float waitTime)
    {
        LookAtEnemy = true;
        yield return new WaitForSeconds(waitTime);
        LookAtEnemy = false;
    }
}