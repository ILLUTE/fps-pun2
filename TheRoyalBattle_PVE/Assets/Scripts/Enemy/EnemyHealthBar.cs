using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform T_HealthBar;

    public Image healthBar;

    private float _MaxHealth;

    public void SetHealth(float maxHealth, float currentHealth)
    {
        _MaxHealth = maxHealth;

        UpdateUI(currentHealth);
    }
    public void UpdateHealthBar(float currentHealth, float deltaChange)
    {
        UpdateUI(currentHealth);
    }

    private void UpdateUI(float currentHealth)
    {
        if (_MaxHealth <= 0)
        {
            return;
        }
        healthBar.fillAmount = currentHealth / _MaxHealth;
    }
}
