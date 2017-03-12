using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class DialogManager : MonoBehaviour
{
    public Canvas dialog;

    public List<NpcText> npcText;
    public List<PlayerText> playerText;


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
                if (dialog.isActiveAndEnabled == false)
                {
                    dialog.gameObject.SetActive(true);
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

    void Start()
    {
        DialogDebugger();
    }

    void DialogDebugger()
    {
        Assert.IsFalse((npcText.Count != playerText.Count), "[Dialog] Length main size are not equal");

        for (int i = 0; i < npcText.Count; ++i)
        {
            Assert.IsFalse((npcText[i].text.Length != playerText[i].text.Length), "[Dialog] Length of Element " + i + " are not the same");
        }
    }
}
#region NpcText Class
[System.Serializable]
public class NpcText
{
    public Text[] text;
}
#endregion

#region Answer Class
[System.Serializable]
public class PlayerText
{
    public Text[] text;
}
#endregion
