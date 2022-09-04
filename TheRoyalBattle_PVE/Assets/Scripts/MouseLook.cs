using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [Range(0,10000.0f)][SerializeField]
    private float mouseSensitivity = 100.0f;

    [SerializeField]
    private Transform playerBody;

    private Vector2 MouseMovement = Vector2.zero;

    private float rollAxis = 0.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mouseX *= Time.deltaTime * mouseSensitivity;
        mouseY *= Time.deltaTime * mouseSensitivity;

        rollAxis -= mouseY;
        rollAxis = Mathf.Clamp(rollAxis, -90, 90);

        transform.localRotation = Quaternion.Euler(rollAxis, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
