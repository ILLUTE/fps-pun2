using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;

    public PhotonView photonView;

    public PhotonView m_PhotonView
    {
        get
        {
            if(photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }

            return photonView;
        }
    }
    public UnityEvent<float, float> OnHealthUpdate;

    public void UpdateHealth(float delta)
    {
        m_PhotonView.RPC("TakeDamage", RpcTarget.All, delta, m_PhotonView.ViewID);
    }

    [PunRPC]
    public void TakeDamage(float delta, int viewID)
    {
        if (!m_PhotonView.ViewID.Equals(viewID)) return;

        currentHealth += delta;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthUpdate?.Invoke(currentHealth, delta);
    }

    public void SetHealth(float _MaxHealth, float _CurrentHealth)
    {
        maxHealth = _MaxHealth;
        currentHealth = _CurrentHealth;
    }
}
