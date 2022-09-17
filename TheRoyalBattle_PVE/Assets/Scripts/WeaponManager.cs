using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Camera playerCam;

    public float weaponRange;

    public float weaponDamage;

    public float bulletPerSecond;

    public ParticleSystem muzzleFlash;

    public ParticleSystem hitSpark;

    public ParticleSystem bloodSpat;

    public Vector2[] weaponRecoil = new Vector2[10];

    public Animator playerAnimator;

    public PlayerManager playerManager;

    private int bulletsShot;

    private int IsShootingParameter = Animator.StringToHash("isShooting");

    private float intervalTime;

    private float lastShotTime;

    public PhotonView photonView;

    public LayerMask ignoring;

    private bool IsShooting;

    private void Awake()
    {
        if (playerCam == null && !PhotonNetwork.InRoom)
        {
            playerCam = Camera.main;
        }

        if(!photonView.IsMine)
        {
            this.enabled = false;
        }
    }

    private void Start()
    {
        intervalTime = 1 / bulletPerSecond;

        if (!photonView.IsMine)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            return;
        }
#if PC
        if (Input.GetMouseButton(0))
        {
            IsShooting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }
#endif
        if (IsShooting)
        {
            Shoot();
        }

        if (playerAnimator.GetBool(IsShootingParameter))
        {
            playerAnimator.SetBool(IsShootingParameter, IsShooting);
        }
    }

    public void StopShooting()
    {
        IsShooting = false;
        bulletsShot = 0;
    }

    public void StartShooting()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            return;
        }
        IsShooting = true;
    }

    private int GetRecoilIndex(int bulletShot)
    {
        return bulletShot % weaponRecoil.Length;
    }

    public void Shoot()
    {
        muzzleFlash.Emit(1);

        if (Time.time < lastShotTime + intervalTime)
        {
            return;
        }

        lastShotTime = Time.time;
        bulletsShot++;
        playerAnimator.SetBool(IsShootingParameter, true);

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, weaponRange,~ignoring))
        {
            playerManager.m_CameraAim.SetCameraShake((Vector3)weaponRecoil[GetRecoilIndex(bulletsShot)], intervalTime);
            Health enemy = hit.collider.GetComponent<Health>();

            ParticleSystem hitParticle;

            if (enemy)
            {
                hitParticle = Instantiate(bloodSpat);
                enemy.UpdateHealth(-1 * weaponDamage);
            }
            else
            {
                hitParticle = Instantiate(hitSpark);
            }
            hitParticle.transform.position = hit.point;
            hitParticle.transform.up = hit.normal;
            hitParticle.transform.SetParent(hit.collider.transform);
            hitParticle.Emit(10);
            Destroy(hitParticle.gameObject, 1f);
        }
    }
}