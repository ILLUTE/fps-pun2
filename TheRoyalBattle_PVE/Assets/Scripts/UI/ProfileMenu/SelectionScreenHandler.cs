using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectionScreenHandler : MonoBehaviour
{
    public SelectionScreens[] selectionScreens = new SelectionScreens[2];

    private PlayerDetails playerDetails;

    private int screenIndex = -1;

    private void Awake()
    {
        ProfileHandler.OnLoadData += OnDataLoaded;
    }

    private void OnDataLoaded()
    {
        playerDetails = ProfileHandler.Instance.PlayerDetails;

        if (playerDetails.avatarID == -1)
        {
            selectionScreens[0].OnScreenOpen();

            screenIndex = 0;
        }
    }

    public void NextButton()
    {
        if (screenIndex >= selectionScreens.Length - 1)
        {
            return;
        }

        selectionScreens[screenIndex].OnScreenClose();
        screenIndex++;
        selectionScreens[screenIndex].OnScreenOpen();
    }

    public void BackButton()
    {
        if (screenIndex > 0)
        {
            screenIndex--;
        }
    }
}
