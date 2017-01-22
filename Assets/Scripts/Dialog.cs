using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Canvas dialog;
    public Text[] text;
    public int dialogueoption;


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
              
                dialog.gameObject.SetActive(true);
               
                {
                    switch (dialogueoption)
                    {
                        case 5:
                            text[4].gameObject.SetActive(false);
                            text[5].gameObject.SetActive(true);
                            dialogueoption++;
                            break;
                        case 4:
                            text[3].gameObject.SetActive(false);
                            text[4].gameObject.SetActive(true);
                            dialogueoption++;
                            break;
                        case 3:
                            text[2].gameObject.SetActive(false);
                            text[3].gameObject.SetActive(true);
                            dialogueoption++;
                            break;
                        case 2:
                            text[1].gameObject.SetActive(false);
                            text[2].gameObject.SetActive(true);
                            dialogueoption++;
                            break;
                        case 1:
                            text[0].gameObject.SetActive(false);
                            text[1].gameObject.SetActive(true);
                            dialogueoption++;
                            break;
                        default:
                            text[5].gameObject.SetActive(false);
                            text[0].gameObject.SetActive(true);
                            dialogueoption++;
                            break;

                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            dialog.gameObject.SetActive(false);
        }
    }
}
        
    

