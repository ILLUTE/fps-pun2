using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public Camera playerCamera;

    private float shakeDuration;
    private float shakeTime;

    private Vector3 shakeIntensity;


    private bool IsCameraShake;

    public void SetCameraShake(Vector3 intensity, float shakeDur)
    {
        shakeDuration = shakeDur;
        shakeIntensity = intensity;

        IsCameraShake = true;
    }

    private void CameraShake()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(shakeIntensity);
    }
    private void Update()
    {
        if (!IsCameraShake)
            return;

        if (shakeTime < shakeDuration)
        {
            shakeTime += Time.unscaledDeltaTime;
            CameraShake();
        }
        else
        {
            shakeTime = 0;
            shakeDuration = -1;
            IsCameraShake = false;
            playerCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
