using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonViewModules : MonoBehaviour
{
    public PlayerLocomotion playerLocomotion;
    public MouseLook playerMouseLook;
    public WeaponManager weapon;
    public CharacterController characterController;
    public CameraAim cameraAim;
    public Canvas mobileInput;

    public void Awake()
    {
        if (PhotonNetwork.InRoom && !GetComponent<PhotonView>().IsMine)
        {
            playerLocomotion.enabled = false;
            playerMouseLook.enabled = false;
            weapon.enabled = false;
         //   characterController.enabled = false;
            cameraAim.enabled = false;
            mobileInput.enabled = false;
        }
    }
}
