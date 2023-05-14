using System.Collections;
using UnityEngine;
using Common;
using Cinemachine;
public class CameraManager : MonoSingleton<CameraManager> {
    public CinemachineVirtualCamera _curActiveCamera;
    public CinemachineVirtualCamera[] CommonCameraList;

    public CinemachineBlendDefinition defaultBlend;

    private CinemachineBrain _brain;

    public void SetDefaultBlend() {
        _brain.m_DefaultBlend = defaultBlend;
    }

    public void SetCustomBlend(CinemachineBlendDefinition customBlend) {
        _brain.m_DefaultBlend = customBlend;
    }

    public void SetCamera(CinemachineVirtualCamera switchVC) {
        switchVC.m_Priority = _curActiveCamera.m_Priority;
        _curActiveCamera.m_Priority = 0;
        _curActiveCamera = switchVC;
    }

    private void Awake() {
        if (CommonCameraList == null || CommonCameraList.Length < 3) {
            DebugLogger.Error(this.name, 
                "Default First Three CommonCameraList Cameras not set.\n" +
                "EyelevelVC_DefaultTrack/EyelevelVC_DefaultBall/DollyTrackVC");
        }

        Transform follow = PlayerController.Instance.transform.Find("FollowAtPoint");
        Transform look = PlayerController.Instance.transform.Find("LookAtPoint");
        if (follow == null || look == null) {
            DebugLogger.Error(this.name, "Follow or lookat point on Player not found.");
        }

        /* EyelevelVC_DefaultTrack */
        CommonCameraList[0].LookAt = look;
        CommonCameraList[0].Follow = look;

        /* EyelevelVC_DefaultBall */
        CommonCameraList[1].LookAt = follow;
        CommonCameraList[1].Follow = follow;

        /* DollyTrackVC */
        CommonCameraList[2].LookAt = follow;
        CommonCameraList[2].Follow = follow;


        if (_curActiveCamera == null) {
            DebugLogger.Error(this.name, "First Camera not set!");
        }
        CinemachineVirtualCamera curhigh = null;
        foreach (CinemachineVirtualCamera virtualCamera in FindObjectsOfType<CinemachineVirtualCamera>()) {
            if (curhigh == null) {
                curhigh = virtualCamera;
                continue;
            }
            if (virtualCamera.m_Priority > curhigh.m_Priority)
                curhigh = virtualCamera;
        }
        if(_curActiveCamera != curhigh) {
            DebugLogger.Error(this.name, "Default Main Camera not Set in CameraManager. Set _curActiveCamera to " + curhigh.name);
        }
    }

    void Start() {
        _brain = FindObjectOfType<CinemachineBrain>();
    }
}
