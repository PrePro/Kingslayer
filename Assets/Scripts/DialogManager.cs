using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class DialogManager : MonoBehaviour
{
    public Canvas dialog;
    public List<NpcText> npcText;
    private List<GameObject> mList;
    //public List<PlayerText> playerText;
    private PlayerStats playerStats;

    private bool Running;
    private Text[] Children;

    private int holder;

    void OnTriggerEnter(Collider col)
    {
        playerStats = col.GetComponent<PlayerStats>();
        Running = true;
        GetFirstCanvas();
    }

    void Update()
    {
        if (Running)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialog.gameObject.activeSelf)
                {
                    Debug.Log("IsActive");
                    dialog.gameObject.SetActive(true);
                }
            }
            TextUpdater(Children);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            dialog.gameObject.SetActive(false);
        }
        Running = false;
    }

    void TextUpdater(Text[] text)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("TextUpdater");
            if (holder != text.Length)
            {
                if (holder - 1 != -1)
                {
                    text[holder - 1].gameObject.SetActive(false);
                }
                text[holder].gameObject.SetActive(true);
                ++holder;
            }
            else
            {
                //Open the buttons
            }
        }
    }

    void GetFirstCanvas()
    {
        for (int i = 0; i <= npcText.Capacity - 1; ++i)
        {
            for (int j = 0; j < npcText[i].text.Length; j++)
            {
                mList.Add(npcText[i].text[j].gameObject);
                mList[i].SetActive(true);
                Children = mList[i].GetComponentsInChildren<Text>(true);
                foreach (Text text in Children) ;
            }
        }
    }

    void Start()
    {
        mList = new List<GameObject>();
    }


    #region ButtonClicks
    public void ButtonOneClick()
    {
        npcText[0].text[0].gameObject.SetActive(true);
        //Delete Button
        //Spawn new canvas connected to this one
    }
    #endregion

}
#region NpcText Class
[System.Serializable]
public class NpcText
{
    public Canvas[] text;
    public Button[] button;
}
#endregion

//#region Answer Class
//[System.Serializable]
//public class PlayerText
//{
//    public Button[] text;
//}
//#endregion
