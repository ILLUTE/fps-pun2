using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CameraAim m_CameraAim;

    public PhotonView photonView;

    public void GetHealthUpdate(float current, float delta)
    {
        if (!photonView.IsMine) return;

        if (delta < 0 && current > 0)
        {
            m_CameraAim.SetCameraShake(new Vector3(-1, 1, 0), .1f);
        }
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            m_CameraAim.playerCamera.gameObject.SetActive(false);
            return;
        }
    }
}

