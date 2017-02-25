//======================================================================================================
// DestroysBulllet.cs
// Description: How projectiles will be destroyed
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

    public float time;
   
    void Start()
    {
        Invoke("OnDestroyed", time);
    }

    public void OnDestroyed()
    {
        Destroy(this.gameObject);
    }
    public void OnCollisionEnter()
    {
        OnDestroyed();
        CancelInvoke();
    }
}
