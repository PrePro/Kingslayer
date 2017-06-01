using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testingScript : MonoBehaviour {

    public GameObject point01;
    public GameObject point02;
    public GameObject point03;
    public GameObject point04;
    public GameObject point05;
    public GameObject point06;
    public GameObject point07;
    public GameObject point08;
    public GameObject point09;
    public GameObject point10;
    public Player privo;
    public Canvas options;
    public Animator privoAnim;
    private bool UIUP = false;
    CoolDownSystem.PlayerState playerState;     // Use this for initialization

    //CoolDownSystem nCD = new CoolDownSystem();
    void Start ()
    {
        options.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            options.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown("escape"))
        {
            options.gameObject.SetActive(false);
        }
	}

    public void toPoint01()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point01.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint02()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point02.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint03()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point03.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint04()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point04.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint05()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point05.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint06()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point06.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint07()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point07.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint08()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point08.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint09()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point09.transform.position;
        privo.gameObject.SetActive(true);
    }
    public void toPoint10()
    {
        privo.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        privo.transform.position = point10.transform.position;
        privo.gameObject.SetActive(true);
    }
}
