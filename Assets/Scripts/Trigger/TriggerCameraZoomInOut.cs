using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class TriggerCameraZoomInOut : TriggerBase
{
    [Header("FollowOffset: 以玩家为中心，设置相机的位置 ps：相机始终跟着并且看向玩家")]
    [Header("Duration： 从现在的位置到设置的位置需要多少时间平滑移动")]
    [Header("EaseType： 以什么函数进行移动，类似与AnimationCurve")]

    [SerializeField] private Vector3 followOffset = Vector3.up;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType = DOTween.defaultEaseType;
    
    protected override void enterEvent()
    {
        base.enterEvent();
        CinemachineTransposer transposer = CameraController.Instance.vcam1CM.GetCinemachineComponent<CinemachineTransposer>();
        DOTween.To(
            () => transposer.m_FollowOffset, 
            x => transposer.m_FollowOffset = x,
            followOffset, duration).SetEase(easeType);
    }
}
