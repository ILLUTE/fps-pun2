using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileHandler : MonoBehaviour
{
    private static ProfileHandler instance;

    public static ProfileHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ProfileHandler>();
            }

            return instance;
        }
    }

    private PlayerDetails playerDetails;

    public PlayerDetails PlayerDetails
    {
        get
        {
            return playerDetails;
        }

        private set { }
    }

    public static event Action OnLoadData;

    private IEnumerator Start()
    {
        playerDetails = SaveDetails.LoadData();

        yield return new WaitForSeconds(2f);

        OnLoadData?.Invoke();
    }

    public void SaveAvatarInfo(int avatarId)
    {
        playerDetails.avatarID = avatarId;
    }
}
