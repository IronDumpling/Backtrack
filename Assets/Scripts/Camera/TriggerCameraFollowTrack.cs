using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class TriggerCameraFollowTrack : TriggerBase
{
    [SerializeField] private float trackCamDuration = 3f;
    [SerializeField] private Ease easeType = DOTween.defaultEaseType;
    public CinemachinePathBase path;
    private void Awake()
    {
        
    }

    protected override void enterEvent()
    {
        CameraController.Instance.isFixOnPath = false;
        CinemachineTrackedDolly vcam2TD =
            CameraController.Instance.vcam2CM.GetCinemachineComponent<CinemachineTrackedDolly>();
        vcam2TD.m_Path = path;
        CameraController.Instance.VCam1ToVCam2();
        DOTween.To(
            () => vcam2TD.m_PathPosition,
            x => vcam2TD.m_PathPosition = x,
            1, trackCamDuration).SetEase(easeType)
            .onComplete += () =>
        {
            CameraController.Instance.isFixOnPath = true;
            CameraController.Instance.VCam2ToVCam1();

        };
    }
    
}
