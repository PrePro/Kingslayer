using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUi : MonoBehaviour
{
    public Image HealthBar;
    public PlayerStats player;
    public Image MoralityBarGood;
    public Image MoralityBarEvil;

    void Awake()
    {
        MoralityBarEvil.fillAmount = 0f;
        MoralityBarGood.fillAmount = 0f;
    }
    void Update()
    {
        HandleHealthBar();
        HandleMoralityBar();
        //if (player.GetHealth() <= 0)
        //{
        //    SceneManager.LoadScene("MainMenu");
        //}
    }

    void HandleHealthBar()
    {
        //float healthBarMap = player.currentHealth / Player.MaxHealth;

        float healthBarMap = player.GetHealth() / player.GetMaxHealth();
        HealthBar.fillAmount = healthBarMap;
    }

    void HandleMoralityBar()
    {
        if(player.Morality == 0)
        {

        }
        else if(player.Morality > 0)
        {
            float moralityBarMapGood = player.GetMorality() / 100f;
            MoralityBarGood.fillAmount = moralityBarMapGood;
            Color evilColor = MoralityBarEvil.color;
            evilColor.a = .7f;
            MoralityBarEvil.color = evilColor;
        }
        else if(player.Morality < 0)
        {
            
            float moralityBarMapEvil = player.GetMorality() / -100f;
            MoralityBarEvil.fillAmount = moralityBarMapEvil;
            Color goodColor = MoralityBarGood.color;
            goodColor.a = .7f;
            MoralityBarGood.color = goodColor;
        }
        
    }
}