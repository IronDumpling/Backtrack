using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class TriggerCameraShake : TriggerBase
{
    public float shakeTimer;
    public float intensity;
    public CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin perlin;
    protected override void enterEvent()
    {
        base.enterEvent();
        Debug.Log("camera shake");
        perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
         
        Invoke(nameof(stopShake),shakeTimer);

    }

    public void stopShake()
    {
       // perlin.m_AmplitudeGain = 0f;
    }
}
