using UnityEngine;
using System.Collections;

public class DestoryBullet : MonoBehaviour {

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
