//======================================================================================================
// Player.cs
// Description: Singleton Player object, only one should exist in game
// Place to store relevant data of player that can be accessed globally by any other object
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{

    private Player() { }

    static Player instance;
    static public Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

	public static Vector3 Position
    {
        get { return Instance.transform.position; }
    }

}
