using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerception : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    public List<GameObject> list;
    private Movement movement;
    public bool LookAtEnemy;
    public float timer;
    public float CoolDown;

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
            //Debug.Log(list[0].name);
        }
        if (timer >= CoolDown)
        {
            if (Input.GetKeyDown(KeyCode.X) && list.Count != 0)
            {
                StartCoroutine(lookatenemy(0.1f));
                timer = 0;
            }

        }

        //if (Input.GetKeyUp(KeyCode.X))
        //{
        //    LookAtEnemy = !LookAtEnemy;
        //}

        if (LookAtEnemy)
        {
            Player.transform.position = list[0].transform.position;
            //movement.stopMovement = true;
            //Player.transform.LookAt(list[0].transform.position);
        }
        else
        {
            //movement.stopMovement = false;
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