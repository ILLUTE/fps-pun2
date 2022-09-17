using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_CurrentHealth;

    public void UpdatedPlayerHealth(float currentHealth, float deltaChange)
    {
        m_CurrentHealth.text = currentHealth.ToString();
    }
}
