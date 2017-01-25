using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Canvas dialog;
    public Text[] text;

    public int holder = 0;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            TextUpdater();
            if (Input.GetKeyDown(KeyCode.E))
                if(dialog.isActiveAndEnabled == false)
                    dialog.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q pressed");
                if (holder - 1 == -1)
                {
                    return;
                }
                text[holder - 1].gameObject.SetActive(false);
                holder = 0;
            }
        }
    }

    void Update()
    {
        if (holder == text.Length)
        {
            text[text.Length - 1].gameObject.SetActive(true);
        }
    }

    void TextUpdater()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("TextUpdater");
            if (holder != text.Length)
            {
                if (holder - 1 != -1)
                    text[holder - 1].gameObject.SetActive(false);
                text[holder].gameObject.SetActive(true);
                holder++;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            holder = 0;
            dialog.gameObject.SetActive(false);
        }
    }
}
