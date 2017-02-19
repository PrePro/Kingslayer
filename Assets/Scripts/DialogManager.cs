using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;


public class DialogManager : MonoBehaviour
{
    public Canvas dialog;
    public List<NpcText> npcText;
    private List<GameObject> mList;
    private PlayerStats playerStats;

    private bool Running;
    private Text[] Children;
    public Button[] tons;
    public Text endText;

    private int holder;
    private bool EndTalk = false;
    private bool mDeleted = false;
    public int mIndex;

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("ENTER");
        if (Running) return; // Makes sure Trigger is called once 
        playerStats = col.GetComponent<PlayerStats>(); // Gets stats for player to change morality
        Running = true;

        if(!mDeleted)
        {
            FillmList();
            GetFirstChildren(mIndex);    
        }
    }

    void Start()
    {
        mList = new List<GameObject>();
        for(int i = 0; i < npcText[mIndex].mbuttons; ++i)
        {
            tons[i].GetComponentInChildren<Text>().text = npcText[mIndex].buttonText[i];
        }
    }


    void Update()
    {
        if (Running)
        {
            for (int i = 0; i < npcText.Capacity - 1; i++)
            {
               if(npcText[i].mbuttons != npcText[i+ 1].canvas.Length)
                {
                    Debug.Log("YOU FUCKING aDSFAFDKGA");
                }
            }
            if(EndTalk && !mDeleted) //Deletion
            {
                for(int i =0; i < npcText.Capacity; ++i)
                {
                    for(int j =0; j < npcText[i].canvas.Length; ++j)
                    Destroy(npcText[i].canvas[j].gameObject);
                }
                for (int i = 0; i < tons.Length; i++)
                {
                    Destroy(tons[i].gameObject);
                }
                mDeleted = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialog.gameObject.activeSelf)
                {
                    //Debug.Log("IsActive");
                    dialog.gameObject.SetActive(true);
                }
            }
            TextUpdater(Children);
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

    void TextUpdater(Text[] text) //Scrolls through the text in canvas
    {
        if(!EndTalk)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holder != text.Length)
            {
                if (holder - 1 != -1)
                {
                    text[holder - 1].gameObject.SetActive(false); // Break
                }
                text[holder].gameObject.SetActive(true);
                ++holder;
            }
            else
            {
                for (int i = 0; i < npcText[mIndex].mbuttons; i++)
                {
                    tons[i].gameObject.SetActive(true);
                }
            }
        }
    }

    void FillmList()
    {
        for (int i = 0; i <= npcText.Capacity - 1; ++i)
        {
            for (int j = 0; j < npcText[i].canvas.Length; j++)
            {
                mList.Add(npcText[i].canvas[j].gameObject);
            }
        }
    }

    void GetFirstChildren(int i)
    {
        for (int j = 0; j < npcText[i].canvas.Length; j++)
        {
            mList[i].SetActive(true); //Set the canvas on
            Children = mList[i].GetComponentsInChildren<Text>(true); // Putting the Children into array
            foreach (Text text in Children) ; //Needs to be here if the text is disabled
        }
    }
    void GetChildrenMult(int i)
    {
        Children = npcText[mIndex + 1].canvas[i].GetComponentsInChildren<Text>(true); // Putting the Children into array
        foreach (Text text in Children) ; //Needs to be here if the text is disabled

    }

    #region ButtonClicks
    public void ButtonClick(int k)
    {
        switch (k)
        {
            case 0:
                playerStats.moralityAoe += npcText[mIndex].morality[0];
                break;
            case 1:
                playerStats.moralityAoe += npcText[mIndex].morality[1];
                break;
            case 2:
                playerStats.moralityAoe += npcText[mIndex].morality[2];
                break;
            default:
                break;
        }

        if (mIndex + 1 == npcText.Capacity)
        {
            Debug.Log("IF");
            EndTalk = true;
            npcText[mIndex].canvas[k].gameObject.SetActive(false);
            foreach (Button b in tons)
            {
                b.gameObject.SetActive(false);
            }
            endText.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Button");
            holder = 0;

            Debug.Log(mList[mIndex].name);
            mList[mIndex].SetActive(false);
            for(int i =0; i < npcText.Capacity; ++i)
            {
                for (int j = 0; j < npcText[mIndex].canvas.Length; j++)
                {
                    npcText[mIndex].canvas[j].gameObject.SetActive(false);
                }
            }
            npcText[mIndex + 1].canvas[k].gameObject.SetActive(true);


            Array.Clear(Children, 0, Children.Length);
            GetChildrenMult(k);
            ++mIndex;
            for (int i = 0; i < npcText[mIndex].mbuttons; ++i)
            {
                tons[i].GetComponentInChildren<Text>().text = npcText[mIndex].buttonText[i];
            }
            foreach (Button b in tons)
            {
                b.gameObject.SetActive(false);
            }
        }
    }
    #endregion


}
#region NpcText Class
[System.Serializable]
public class NpcText
{
    public Canvas[] canvas;
    public string[] buttonText;
    public int mbuttons;
    public int[] morality;
}
#endregion