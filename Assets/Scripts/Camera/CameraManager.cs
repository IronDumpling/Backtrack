using System.Collections;
using UnityEngine;
using Common;
using Cinemachine;
public class CameraManager : MonoSingleton<CameraManager> {
    [SerializeField]
    private CinemachineVirtualCamera _curActiveCamera;
    public CinemachineVirtualCamera[] CommonCameraList;

    public CinemachineBlendDefinition defaultBlend;

    private CinemachineBrain _brain;

    public void SetDefaultBlend() {
        _brain.m_DefaultBlend = defaultBlend;
    }

    public void SetCustomBlend(CinemachineBlendDefinition customBlend) {
        _brain.m_DefaultBlend = customBlend;
    }

    public void SetCommonCamera(in int idx) {
        _curActiveCamera.m_Priority = 0;
        if (idx >= CommonCameraList.Length) {
            DebugLogger.Error(this.name, "TriggerCamera's CommonCameraIdx switched During Runtime!");
        }
        _curActiveCamera = CommonCameraList[idx];
        _curActiveCamera.m_Priority = 10;
    }

    public void SetCustomCamera(CinemachineVirtualCamera switchVC) {
        _curActiveCamera.m_Priority = 0;
        _curActiveCamera = switchVC;
        _curActiveCamera.m_Priority = 10;
    }

    private void Awake() {
        if (CommonCameraList == null || CommonCameraList[0] == null) {
            DebugLogger.Error(this.name, "Default CommonCameraList first camera not set.");
        }
    }

    void Start() {
        _curActiveCamera = CommonCameraList[0];
        _curActiveCamera.m_Priority = 10;

        _brain = FindObjectOfType<CinemachineBrain>();
    }
}
