using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    bool isBlocking;
    Renderer meshRenderer;
    Color currentColor;

    public float alphaTransparency = 0.3f;
    public float opaqueValue = 1.0f;
    public float fadeSpeed = 0.95f;

    public bool Blocking
    {
        set { isBlocking = value; }
    }
	// Use this for initialization
	void Start ()
    {
        meshRenderer = GetComponent<Renderer>();
        currentColor = meshRenderer.material.color;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CheckIfBlocking();

        if (isBlocking)
        {
            FadeAway();
        }
        else
        {
            Reappear();
        }
	}

    void CheckIfBlocking()
    {
        Ray cameraToPlayer = new Ray(Camera.main.transform.position, (Player.Position - Camera.main.transform.position).normalized);

        //First check if player was first to be hit
        isBlocking = false;
         var hits = Physics.RaycastAll(cameraToPlayer);

        bool isHit = false;
        float playerDistance = 0.0f;
        float obstacleDistance = 0.0f;
        foreach(var hit in hits)
        {
            if(hit.collider.gameObject == gameObject)
            {
                isHit = true;
                obstacleDistance = hit.distance;
            }
            if(hit.collider.gameObject.tag == "Player")
            {
                playerDistance = hit.distance;
            }
        }

        if(isHit && obstacleDistance < playerDistance)
        {
            isBlocking = true;
        }
    }

    void FadeAway()
    {
        if (Mathf.Approximately(currentColor.a, alphaTransparency))
        {
            return;
        }
        currentColor.a = Mathf.Lerp(currentColor.a, alphaTransparency, fadeSpeed * Time.deltaTime);
        meshRenderer.material.color = currentColor;
    }

    void Reappear()
    {
        if(Mathf.Approximately(currentColor.a, opaqueValue))
        {
            return;
        }
        currentColor.a = Mathf.Lerp(currentColor.a, opaqueValue, fadeSpeed * Time.deltaTime);
        meshRenderer.material.color = currentColor;
    }
}
