using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeInTrans : MonoBehaviour {
    public Image myImage;
    public Color myColor;
    public float myAlpha = 1;
    public float transTime = 1;
    // Use this for initialization
    void Start () {
        myImage = GetComponent<Image>();
        myImage.color = myColor;
        myColor.a = myAlpha;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("faade");

    }

    IEnumerator faade()
    {
        while (transTime > 0)
        {
            myAlpha -= 0.1f;
            transTime -= .1f;
            //Debug.Log(time);
            yield return new WaitForSeconds(.1f);
        }
    }
}
