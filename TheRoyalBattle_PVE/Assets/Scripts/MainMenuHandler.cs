using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public CanvasGroup OnLoadingScreen;
    public CanvasGroup OnLoadedScreen;

    public Image loader;

    private void Awake()
    {
        NetworkingManager.ConnectToServer += ConnectingToServer;
        NetworkingManager.ConnectedToMaster += ConnectedToMaster;
        NetworkingManager.JoinedLobby += JoinedLobby;
        NetworkingManager.JoinedRoom += JoinedRoom;
    }

    private void JoinedRoom()
    {
        
    }

    private void JoinedLobby()
    {
        loader.fillAmount = 1;
        OnLoadingScreen.alpha = 0;
        OnLoadingScreen.gameObject.SetActive(false);
        OnLoadedScreen.gameObject.SetActive(true);
        OnLoadedScreen.alpha = 1;
    }

    private void ConnectedToMaster()
    {
        loader.fillAmount = .5f;
    }

    private void ConnectingToServer()
    {
        OnLoadedScreen.gameObject.SetActive(false);
        OnLoadingScreen.gameObject.SetActive(true);
        OnLoadingScreen.alpha = 1;

        loader.fillAmount = 0.1f;
    }


}
