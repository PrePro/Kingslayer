using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUi : MonoBehaviour
{
    public Image HealthBar;
    public PlayerStats player;
 
    void Start()
    {
        player.startPosition = player.transform.position;
    }

     void Update()
{
    HandleHealthBar();
    
}

void HandleHealthBar()
{
    //float healthBarMap = player.currentHealth / Player.MaxHealth;

    float healthBarMap = player.GetHealth() / player.GetMaxHealth();
    HealthBar.fillAmount = healthBarMap;
}
 }