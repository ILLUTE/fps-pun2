using System;
using System.Collections;
using System.Collections.Generic;
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


    private void Update()
    {
        m_AxisMovement.x = Input.GetAxis("Horizontal");

        m_AxisMovement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        CheckForRunning();
    }

    private void CheckForRunning()
    {
        currentSpeed = IsRunning() ? sprintSpeed : walkSpeed;
    }

    private void Jump()
    {
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
