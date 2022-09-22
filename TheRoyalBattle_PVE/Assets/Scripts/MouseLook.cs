using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [Range(0, 10000.0f)]
    [SerializeField]
    private float mouseSensitivity = 100.0f;

    [SerializeField]
    private Transform playerBody, characterBody;

    [SerializeField]
    private Transform childPlayer;

    private float rollAxis = 0.0f;

    public PhotonView m_PhotonView;

    private int rightFingerId = -1;

    private float mouseX;
    private float mouseY;

    private Vector2 startPosition;
    private Touch cachedTouch;

    public bool IsNonPlayer;

    private void Start()
    {
        if (!m_PhotonView.IsMine)
        {
            this.enabled = false;
            IsNonPlayer = true;
        }
        else
        {
            IsNonPlayer = false;
        }
        rightFingerId = -1;
#if MYPC
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    void Update()
    {
        if (PhotonNetwork.InRoom && !m_PhotonView.IsMine)
        {
            return;
        }
#if !MYPC
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                if (rightFingerId == -1 && t.position.x > Screen.width / 2)
                {
                    rightFingerId = 1;
                    startPosition = t.position;
                }
            }

            if (t.phase == TouchPhase.Moved)
            {
                if (t.position.x > Screen.width / 2)
                {
                    mouseX = (t.position.x - startPosition.x)/100 * Time.deltaTime * mouseSensitivity/10;
                    mouseY = (t.position.y - startPosition.y)/100 * Time.deltaTime * mouseSensitivity/10;
                }
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                rightFingerId = -1;
                startPosition = Vector2.zero;

                mouseX = 0;
                mouseY = 0;
            }
        }
#else
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        mouseX *= Time.deltaTime * mouseSensitivity;
        mouseY *= Time.deltaTime * mouseSensitivity;

#endif
        rollAxis -= mouseY;
        rollAxis = Mathf.Clamp(rollAxis, -90, 90);

        if (!IsNonPlayer)
        {
            childPlayer.localRotation = Quaternion.Euler(rollAxis, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            characterBody.Rotate(Vector3.up * mouseX);
        }
    }
}