using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraFollowPlayer : TriggerBase
{
    [SerializeField] private float duration = 2f;
    protected override void enterEvent()
    {
        CameraController.Instance.isFixOnPath = false;
        Invoke(nameof(setFixOnPath) , duration);
    }

    void setFixOnPath()
    {
        CameraController.Instance.isFixOnPath = true;
    }
}
