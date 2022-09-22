using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class MeshHandler : MonoBehaviour
{
    [SerializeField]
    private PhotonView m_PhotonView;

    [SerializeField]
    private GameObject firstPersonMesh, thirdPersonMesh;

    [SerializeField]
    private Transform weapon, thirdPersonWeaponGrip, firstPersonWeaponGrip;
    private void Start()
    {
        if (!m_PhotonView.IsMine)
        {
            thirdPersonMesh.SetActive(true);
            weapon.SetParent(thirdPersonWeaponGrip);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            firstPersonMesh.SetActive(true);
            weapon.SetParent(firstPersonWeaponGrip);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
