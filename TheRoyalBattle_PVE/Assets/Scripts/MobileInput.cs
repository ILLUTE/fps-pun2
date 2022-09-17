using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;

    public Button JumpButton;

    public EventTrigger Shoot;

    public void AddMethod(Action onAction)
    {
        JumpButton.onClick.AddListener(() => { onAction?.Invoke(); });
    }

    public void Shooting(Action onShoot)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { onShoot?.Invoke(); });
        Shoot.triggers.Add(entry);
    }

    public void StopShooting(Action notShooting)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { notShooting?.Invoke(); });
        Shoot.triggers.Add(entry);
    }
}
