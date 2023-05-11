using UnityEngine;
using Cinemachine;

public class TriggerCamera : TriggerBase {

    [SerializeField] protected int _CommonCamIdx = -1;
    [SerializeField] protected CinemachineVirtualCamera _SelectVC;

    [SerializeField] protected bool _SetCustomBlend;
    [SerializeField] protected CinemachineBlendDefinition _CustomBlend;

    private CameraManager _camManager;

    protected override void enterEvent() {
        base.enterEvent();

        if (_SetCustomBlend) _camManager.SetCustomBlend(_CustomBlend);
        else _camManager.SetDefaultBlend();

        _camManager.SetCamera(_SelectVC);
    }

    protected virtual void _awake() { }

    private void Awake() {
        var camList = CameraManager.Instance.CommonCameraList;
        if (_CommonCamIdx >= camList.Length) {
            DebugLogger.Error(this.name, "Selected CmmonCamera from list out of Bound.");
        }
        if(_CommonCamIdx < 0 && _SelectVC == null) {
            DebugLogger.Error(this.name, "CustomCamera not set.");
        }
        if (_SelectVC == null) _SelectVC = camList[_CommonCamIdx];
        _awake();
    }

    private void Start() {
        _camManager = CameraManager.Instance;
    }
}
