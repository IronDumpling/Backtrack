using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [HideInInspector] public Transform vcam1;
    [HideInInspector] public Transform vcam2;
    [HideInInspector] public CinemachineVirtualCamera vcam1CM;
    [HideInInspector] public CinemachineVirtualCamera vcam2CM;
    private void Awake()
    {
        vcam1 = this.transform.Find("CM vcam1");
        vcam2 = this.transform.Find("CM vcam2");
        if(vcam1 == null) Debug.LogError("找不到vcam1");
        if(vcam2 == null) Debug.LogError("找不到vcam1");

        vcam1CM = vcam1.GetComponent<CinemachineVirtualCamera>();
        vcam2CM = vcam2.GetComponent<CinemachineVirtualCamera>();
        Transform lookAtPoint = PlayerController.Instance.transform.Find("LookAtPoint");
        if(lookAtPoint == null) Debug.LogError("Player里没有LookAtPoint，摄像机无法跟踪");
        vcam1CM.Follow = lookAtPoint;
        vcam1CM.LookAt = lookAtPoint;
        
        vcam2CM.Follow = lookAtPoint;
        vcam2CM.LookAt = lookAtPoint;
        
        CinemachineTransposer transposer1 = vcam1CM.GetCinemachineComponent<CinemachineTransposer>();
        CinemachineTransposer transposer2 = vcam2CM.GetCinemachineComponent<CinemachineTransposer>();
        transposer2.m_FollowOffset = transposer1.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
