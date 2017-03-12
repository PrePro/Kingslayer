using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUi : MonoBehaviour
{
    public Image HealthBar;
    public PlayerStats player;

    void Update()
    {
        HandleHealthBar();
        if (player.GetHealth() <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void HandleHealthBar()
    {
        //float healthBarMap = player.currentHealth / Player.MaxHealth;

        float healthBarMap = player.GetHealth() / player.GetMaxHealth();
        HealthBar.fillAmount = healthBarMap;
    }
}