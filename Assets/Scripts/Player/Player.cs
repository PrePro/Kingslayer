using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    private PlayerStats stats;
    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        if(stats == null)
        {
            Debug.Log("ASDA");
        }
    }
    void Update()
    {
        isDead = stats.isDead;
    }

    private Player() { }

    static Player instance;
    static public bool isDead;
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
    public static bool DeathState
    {
        get { return Instance.stats.isDead; }
    }
}
