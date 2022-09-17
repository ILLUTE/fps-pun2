using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField]
    [Space(20)]
    [Header("Player Movements")]
    [Range(1, 10)]
    private float walkSpeed, sprintSpeed, currentSpeed;

    [SerializeField]
    [Range(0.1f, 400f)]
    private float jumpHeight;

    [SerializeField]
    [Range(0.1f, 1.0f)]
    private float airControl;

    [SerializeField]
    private bool IsJumping;


    [Header("References")]
    [SerializeField]
    private CharacterController controller;
    //------------------------------------
    [Space(20)]
    [Header("GroundedCheck")]
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float groundRadius;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private bool IsGrounded;

    [SerializeField]
    private float gravity = -9.81f;
    // -----------------------------------
    [Space(20)]
    private Vector2 m_AxisMovement;



    private Vector3 verticalVelocity;
    private Vector3 characterMovementOnGround;

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

    public FixedJoystick floatingJoystick;
    public Canvas MobileInput;

    private void Start()
    {
        if(!m_PhotonView.IsMine)
        {
            this.enabled = false;
            return;
        }
#if PC
        MobileInput.gameObject.SetActive(false);
#else
        if (m_PhotonView.IsMine)
        {
            MobileInput.gameObject.SetActive(true);
        }
#endif
    }
    private void Update()
    {

        if(PhotonNetwork.InRoom && !m_PhotonView.IsMine)
        {
            return;
        }
#if PC
        m_AxisMovement.x = Input.GetAxis("Horizontal");

        m_AxisMovement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

#else
        m_AxisMovement.x = floatingJoystick.Horizontal;
        m_AxisMovement.y = floatingJoystick.Vertical;
#endif
        CheckForRunning();
    }

    private void CheckForRunning()
    {
        currentSpeed = IsRunning() ? sprintSpeed : walkSpeed;
    }

    public void Jump()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            return;
        }
        if (IsJumping)
        {
            return;
        }

        float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * jumpHeight);

        verticalVelocity.y = jumpVelocity;

        IsJumping = true;
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift) && !IsJumping;
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        IsJumping = !IsGrounded;

        if (IsGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2.0f;
        }

        characterMovementOnGround = transform.forward * m_AxisMovement.y + transform.right * m_AxisMovement.x;

        characterMovementOnGround *= currentSpeed * Time.fixedDeltaTime;

        verticalVelocity.y += gravity * Time.fixedDeltaTime;

        controller.Move(verticalVelocity * Time.fixedDeltaTime);

        controller.Move(characterMovementOnGround);
    }
}
