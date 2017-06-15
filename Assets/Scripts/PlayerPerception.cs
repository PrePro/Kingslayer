using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerception : MonoBehaviour
{
    public GameObject Target;
    [HideInInspector]
    public List<GameObject> list = null;
    [HideInInspector]
    public List<AI_KnightAttack> KnightAttack = null;
    private AI_KnightAttack selectedKnight;
    [HideInInspector]
    public bool LookAtEnemy;
    private float timer;
    private float timer2;
    public float CoolDown;
    public int index;

    public Transform theParent;
    //public Vector3 NewPos;
    void Update()
    {
        if (list.Count != 0)
        {
            KnightAttack[index].TurnOnHighlight();
        }

        if (timer2 < 0.5)
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
            if (Player.ControllerState == Player.Controller.KeyBoard)
            {
                Target.transform.position = list[index].transform.position;
            }
            else
            {
                Vector3 lookPos = list[index].transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
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
                    KnightAttack[index - 1].TurnOffHighlight();
                }

            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                if (index - 1 >= 0)
                {
                    index -= 1;
                    KnightAttack[index + 1].TurnOffHighlight();
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
            if (x == 0)
            {
                timer2 = 0.2f;
            }

            if (x >= 0.5)
            {
                if (timer2 >= 0.2)
                {
                    if (index + 1 < list.Count)
                    {
                        index += 1;
                        KnightAttack[index - 1].TurnOffHighlight();
                        timer2 = 0;
                    }
                }
            }
            if (x <= -0.5)
            {
                if (timer2 >= 0.2)
                {
                    if (index - 1 >= 0)
                    {
                        index -= 1;
                        KnightAttack[index + 1].TurnOffHighlight();
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
                KnightAttack.Add(col.gameObject.GetComponentInParent<AI_KnightAttack>());

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

                if(col.gameObject.GetComponentInParent<AI_KnightAttack>().Highlight.activeSelf)
                {
                    col.GetComponentInParent<AI_KnightAttack>().TurnOffHighlight();
                }

                KnightAttack.Remove(col.gameObject.GetComponentInParent<AI_KnightAttack>());

                if (index + 1 > list.Count)
                {
                    index = 1;
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