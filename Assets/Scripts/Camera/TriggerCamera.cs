using UnityEngine;
using Cinemachine;

public class TriggerCamera : TriggerBase {

    [SerializeField] protected int CommonCamIdx = -1;
    [SerializeField] protected CinemachineVirtualCamera _CustomVC;

    [SerializeField] protected bool _SetCustomBlend;
    [SerializeField] protected CinemachineBlendDefinition _CustomBlend;

    private CameraManager _camManager;

    protected override void enterEvent() {
        base.enterEvent();

        if (_SetCustomBlend) {
            _camManager.SetCustomBlend(_CustomBlend);
        }
        else _camManager.SetDefaultBlend();

        if (_CustomVC != null) {
            _camManager.SetCustomCamera(_CustomVC);
        }
        else _camManager.SetCommonCamera(CommonCamIdx);
    }

    protected virtual void _awake() { }

    private void Awake() {
        if (CommonCamIdx >= CameraManager.Instance.CommonCameraList.Length) {
            DebugLogger.Error(this.name, "Selected CmmonCamera from list out of Bound.");
        }
        if(CommonCamIdx < 0 && _CustomVC == null) {
            DebugLogger.Error(this.name, "CustomCamera not set.");
        }
        _awake();
    }

    private void Start() {
        _camManager = CameraManager.Instance;
    }
}
