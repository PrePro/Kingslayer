using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{

    bool isBlocking;
    Renderer meshRenderer;
    Color[] currentColors;
    public float alphaTransparency = 0.3f;
    public float opaqueValue = 1.0f;
    public float fadeSpeed = 0.95f;
    public GameObject[] obstacles;

    public bool Blocking
    {
        set { isBlocking = value; }
    }
	// Use this for initialization
	void Start ()
    {
        meshRenderer = GetComponent<Renderer>();
        var materials = meshRenderer.materials;
        currentColors = new Color[materials.Length];

        for(int i = 0; i < materials.Length; ++i)
        {
            currentColors[i] = materials[i].color;
        }
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

    bool GameObjectIsObstacle(GameObject other)
    {
        if(other == gameObject)
        {
            return true;
        }
        if(obstacles == null)
        {
            return false;
        }
        for(int i = 0; i < obstacles.Length; ++i)
        {
            if(other == obstacles[i])
            {
                return true;
            }
        }

        return false;

    }

    void CheckIfBlocking()
    {
        Ray cameraToPlayer = new Ray(Camera.main.transform.position, (Player.Position - Camera.main.transform.position).normalized);

        //First check if player was first to be hit
        isBlocking = false;
         var hits = Physics.RaycastAll(cameraToPlayer);

        bool isHit = false;
        float playerDistance = 0.0f;
        float obstacleDistance = float.MaxValue;
        foreach(var hit in hits)
        {
            if(GameObjectIsObstacle(hit.collider.gameObject))
            {
                isHit = true;
                if(obstacleDistance > hit.distance)
                {
                    obstacleDistance = hit.distance;
                }
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
        if (Mathf.Approximately(currentColors[0].a, alphaTransparency))
        {
            return;
        }
        float alpha = Mathf.Lerp(currentColors[0].a, alphaTransparency, fadeSpeed * Time.deltaTime);

        var materials = meshRenderer.materials;
        for(int i = 0; i < materials.Length; ++i)
        {
            currentColors[i].a = alpha;
            materials[i].color = currentColors[i];
        }
    }

    void Reappear()
    {
        if(Mathf.Approximately(currentColors[0].a, opaqueValue))
        {
            return;
        }
        float alpha = Mathf.Lerp(currentColors[0].a, opaqueValue, fadeSpeed * Time.deltaTime);

        var materials = meshRenderer.materials;
        for (int i = 0; i < materials.Length; ++i)
        {
            currentColors[i].a = alpha;
            materials[i].color = currentColors[i];
        }
    }
}
