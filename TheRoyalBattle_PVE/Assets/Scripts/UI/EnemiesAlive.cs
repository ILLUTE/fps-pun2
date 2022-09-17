using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesAlive : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_EnemiesAlive;

    public void DisplayEnemies(int enemiesAlive)
    {
        m_EnemiesAlive.text = string.Format("x {0}", enemiesAlive);
    }
}
