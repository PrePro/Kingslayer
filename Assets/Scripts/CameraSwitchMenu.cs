using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchMenu : MonoBehaviour {
    public GameObject cam1;
    public GameObject cam2;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public GameObject House;
    public GameObject City;
    public GameObject Crypt;
    public GameObject Village;
    public GameObject Barracks;
    public GameObject Castle;
    public GameObject House1;
    public GameObject City1;
    public GameObject Crypt1;
    public GameObject Village1;
    public GameObject bgImage;
    // Use this for initialization
    void Awake () {

        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);

        House.SetActive(false);
        City.SetActive(false);
        Crypt.SetActive(false);
        Village.SetActive(false);
        Barracks.SetActive(false);
        Castle.SetActive(false);
        House1.SetActive(false);
        City1.SetActive(false);
        Crypt1.SetActive(false);
        Village1.SetActive(false);
        bgImage.SetActive(false);
        //cam2.gameObject.SetActive(false);
        //cam1.gameObject.SetActive(true);
        cam1.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void enableCamera1()
    {
        Debug.Log("enableCam1");
        cam1.SetActive(true);
        cam2.SetActive(false);

        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);

        House.SetActive(false);
        City.SetActive(false);
        Crypt.SetActive(false);
        Village.SetActive(false);
        Barracks.SetActive(false);
        Castle.SetActive(false);


    }

    public void enableCamera2()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);

        House.SetActive(true);
        City.SetActive(true);
        Crypt.SetActive(true);
        Village.SetActive(true);
        Barracks.SetActive(true);
        Castle.SetActive(true);

        Debug.Log("enableCam2");
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    public void levelSelect()
    {
        House1.SetActive(true);
        City1.SetActive(true);
        Crypt1.SetActive(true);
        Village1.SetActive(true);
        bgImage.SetActive(true);
    }
    public void levelSelectOff()
    {
        House1.SetActive(false);
        City1.SetActive(false);
        Crypt1.SetActive(false);
        Village1.SetActive(false);
        bgImage.SetActive(false);
    }
}
