using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerception : MonoBehaviour
{
    public GameObject Target;
    [HideInInspector]
    public List<GameObject> list;
    private Movement movement;
    [HideInInspector]
    public bool LookAtEnemy;
    private float timer;
    private float timer2;
    public float CoolDown;
    public   int index;

    public Transform theParent;
    //public Vector3 NewPos;

    void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    void Update()
    {
        if (0.5 > timer2)
        {
            timer2 += Time.deltaTime;
        }

        if (CoolDown + 1 > timer)
        {
            timer += Time.deltaTime;
        }

        ControllerUpdater();
        KeyBoardUpdater();

        if (timer >= CoolDown)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Joystick1Button9) && list.Count != 0)
            {
                StartCoroutine(lookatenemy(0.1f));
                timer = 0;
            }
        }

        if (LookAtEnemy)
        {
            if (movement.mController == Movement.Controller.KeyBoard)
            {
                Target.transform.position = list[index].transform.position;
            }
            else
            {
                Debug.Log("Controller loook at");
                var lookPos = list[index].transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                theParent.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
            }

        }
    }

    void KeyBoardUpdater()
    {
        if (list.Capacity != 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (index + 1 < list.Count)
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
    }

    void ControllerUpdater()
    {
        float x = Input.GetAxis("RightVertical");
        //Debug.Log(x);
        if (list.Capacity != 0)
        {
            if (x >= 0.5)
            {
                if (timer2 >= 0.2)
                {
                    if (index + 1 < list.Count)
                    {
                        index += 1;
                        timer2 = 0;
                    }
                }
            }

            else if (x <= -0.5)
            {
                if (timer2 >= 0.2)
                {
                    if (index - 1 >= 0)
                    {
                        index -= 1;
                        timer2 = 0;
                    }
                }
            }
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
                if (index > list.Count)
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