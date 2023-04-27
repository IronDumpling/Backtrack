using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Vcam1 负责跟踪LookAtPoint
/// Vcam2 负责Dolly Track
/// </summary>
public class CameraController : MonoSingleton<CameraController>
{
    #region CamProperty
    [HideInInspector] public Transform vcam1;
    [HideInInspector] public Transform vcam2;
    [HideInInspector] public CinemachineVirtualCamera vcam1CM;
    [HideInInspector] public CinemachineVirtualCamera vcam2CM;
    [HideInInspector] public Transform curVcam;
    #endregion

    [SerializeField] public bool isFixOnPath = true;
    [SerializeField] private float switchVcamDuration = 0.5f;
    [SerializeField] private Vector3 defaultLookAtPointPos = Vector3.up;
    
    [SerializeField] private float camFocusValue = 1f;
    [SerializeField] private float camUnFocusValue = 0.0001f;

    private Transform _playerTr;
    private PlayerMotor _playerMotor;
    private Transform _lookAtPoint;
    private CinemachineMixingCamera _mixingCamera;

    private Tween cam1WeightSwitchTween = null;
    private Tween cam2WeightSwitchTween = null;
    private void Awake()
    {
        _mixingCamera = this.GetComponent<CinemachineMixingCamera>();
        vcam1 = this.transform.Find("CM vcam1");
        vcam2 = this.transform.Find("CM vcam2");
        if(vcam1 == null) Debug.LogError("找不到vcam1");
        if(vcam2 == null) Debug.LogError("找不到vcam2");

        vcam1CM = vcam1.GetComponent<CinemachineVirtualCamera>();
        vcam2CM = vcam2.GetComponent<CinemachineVirtualCamera>();//获取vcam2的CinemachineVirtualCamera组件
        _playerTr = PlayerController.Instance.transform;
        _lookAtPoint = _playerTr.Find("LookAtPoint");
        _playerMotor = _playerTr.GetComponent<PlayerMotor>();
        if(_lookAtPoint == null) Debug.LogError("Player里没有LookAtPoint，摄像机无法跟踪");
        vcam1CM.Follow = _lookAtPoint;
        vcam1CM.LookAt = _lookAtPoint;
        
        vcam2CM.Follow = _lookAtPoint;
        vcam2CM.LookAt = _lookAtPoint;
        
        CinemachineTransposer transposer1 = vcam1CM.GetCinemachineComponent<CinemachineTransposer>();//获取vcam1的transposer组件


        curVcam = vcam1;
    }

    public void VCam1ToVCam2()
    {
        cam1WeightSwitchTween = DOTween.To(
            () => _mixingCamera.GetWeight(0),
            x => _mixingCamera.SetWeight(0, x),
            camUnFocusValue, switchVcamDuration);
        cam2WeightSwitchTween = DOTween.To(
            () => _mixingCamera.GetWeight(1),
            x => _mixingCamera.SetWeight(1, x),
            camFocusValue, switchVcamDuration);
    }
    
    public void VCam2ToVCam1()
    {
        cam1WeightSwitchTween = DOTween.To(
            () => _mixingCamera.GetWeight(0),
            x => _mixingCamera.SetWeight(0, x),
            camFocusValue, switchVcamDuration);
        cam2WeightSwitchTween = DOTween.To(
            () => _mixingCamera.GetWeight(1),
            x => _mixingCamera.SetWeight(1, x),
            camUnFocusValue, switchVcamDuration);
        cam2WeightSwitchTween.onComplete += () =>
        {
            vcam2CM.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixOnPath)
        {
            FixCameraOnPath();
        }
        else
        {
            _lookAtPoint.localPosition = defaultLookAtPointPos;
        }
    }

    
    void FixCameraOnPath()
    {
        RaycastHit hit;
        if (_playerMotor.RayCastBottom(out hit))
        {
            
            Transform hitTr = hit.collider.transform;
            //local position

            Vector3 localHitPoint = _playerTr.InverseTransformPoint(hitTr.position);
            _lookAtPoint.localPosition = new Vector3(localHitPoint.x, _lookAtPoint.localPosition.y, _lookAtPoint.localPosition.z);
            
        }
        
    }
    

}
