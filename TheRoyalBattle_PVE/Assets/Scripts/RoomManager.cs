using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class RoomManager : MonoBehaviourPunCallbacks
{
    private static RoomManager instance;

    public static RoomManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<RoomManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(Instance);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene,LoadSceneMode loadMode)
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-3, 3), 2, Random.Range(-3, 3));

        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("First_Person_Player", spawnPoint, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load("First_Person_Player"), spawnPoint, Quaternion.identity);
        }
    }
}
