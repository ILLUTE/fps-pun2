using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    public static event Action ConnectToServer;
    public static event Action ConnectedToMaster;
    public static event Action JoinedLobby;
    public static event Action JoinedRoom;

    private void Start()
    {
        ConnectToServer?.Invoke();

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        ConnectedToMaster?.Invoke();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        JoinedLobby?.Invoke();
    }

    public void FindMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void FindCustomRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        CreateRoom();

    }

    private void CreateRoom()
    {
        string roomId = string.Format("{0} room", UnityEngine.Random.Range(0, 999));

        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4,
            PublishUserId = true,
        };

        PhotonNetwork.CreateRoom(roomId, roomOptions);

        Debug.LogWarning("Room Created " + roomId);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.LoadLevel(2);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
