using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CurrentRoundUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_CurrentRound;

    public void Display(int gameRounds)
    {
        m_CurrentRound.text = string.Format("Wave {0}", gameRounds);
    }
}
