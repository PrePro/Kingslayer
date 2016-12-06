using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float maxhealth = 100;
    public float health = 100;

    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            health--;
        }
    }
}
