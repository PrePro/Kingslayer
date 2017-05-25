using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Simple_Dialogue : MonoBehaviour

{
    public Canvas dialog;
    public Text[] text;

    private int holder = 0;
    private bool Collid;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Collid = true;
                if (dialog.isActiveAndEnabled == false)
                {
                    dialog.gameObject.SetActive(true);
                }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q pressed");
                if (holder - 1 == 1)
                {
                    return;
                }
                text[holder - 1].gameObject.SetActive(false);
                holder = 0;
                dialog.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Collid == true)
        {
            TextUpdater();
        }
        if (holder == text.Length)
        {
            text[text.Length - 1].gameObject.SetActive(true);
        }
    }

    void TextUpdater()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(text.Length);
            if (holder != text.Length)
            {
                if (holder - 1 != -1)
                {
                    text[holder - 1].gameObject.SetActive(false);
                }
                text[holder].gameObject.SetActive(true);
                ++holder;
            }
            dialog.gameObject.SetActive(true);
        }
    }


    void OnTriggerExit(Collider col)

    {
        if (col.gameObject.tag == "Player")
        {
            foreach (Text a in text)
            {
                a.gameObject.SetActive(false);
            }
            dialog.gameObject.SetActive(false);
            Collid = false;
            holder = 0;
        }
    }
}
