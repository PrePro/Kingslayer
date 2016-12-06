using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Image HealthBar;

    void Update()
    {
        HandleHealthBar();
    }

    void HandleHealthBar()
    {
        float healthBarMap = Player.Health / Player.MaxHealth;
        Debug.Log(Player.Health);
        Debug.Log(healthBarMap);
        HealthBar.fillAmount = healthBarMap;
    }


}
