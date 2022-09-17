using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    private static LogManager instance;

    public static LogManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LogManager>();
            }

            return instance;
        }
    }

    public TextMeshProUGUI logMessage;

    private void Start()
    {
        logMessage.text = string.Empty;
    }
    public void GoLog(string message)
    {
        logMessage.text += message + "\n";
    }
}
