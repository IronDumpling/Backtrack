using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

[Serializable]
public class ParamLerp<T> {
    public bool isInable = false;
    public T toVal;
    public float lerpTime = 1f;
    public Ease easeType = DOTween.defaultEaseType;
}

public class TriggerCameraParamLerp : TriggerBase {
    [SerializeField] private CinemachineVirtualCamera cam;
    private CinemachineComposer composer;
    private CinemachineTransposer transposer;

    public ParamLerp<float> FOV;
    public ParamLerp<float> FarPlane;
    public ParamLerp<float> NearPlane;
    public ParamLerp<Vector3> FollowOffset;
    public ParamLerp<Vector3> LookOffset;

    protected override void enterEvent() {

        // FOV lerp
        if (FOV.isInable) {
            DOTween.To(
                () => cam.m_Lens.FieldOfView,
                x => cam.m_Lens.FieldOfView = x,
                FOV.toVal,
                FOV.lerpTime
            ).SetEase(FOV.easeType);
        }

        // Far Plane
        if (FarPlane.isInable) {
            DOTween.To(
                () => cam.m_Lens.FarClipPlane,
                x => cam.m_Lens.FarClipPlane = x,
                FarPlane.toVal,
                FarPlane.lerpTime
            ).SetEase(FarPlane.easeType);
        }

        // Near Plane
        if (NearPlane.isInable) {
            DOTween.To(
                () => cam.m_Lens.NearClipPlane,
                x => cam.m_Lens.NearClipPlane = x,
                NearPlane.toVal,
                NearPlane.lerpTime
            ).SetEase(NearPlane.easeType);
        }


        // FollowOffset
        if (FollowOffset.isInable) {
            DOTween.To(
                () => transposer.m_FollowOffset,
                x => transposer.m_FollowOffset = x,
                FollowOffset.toVal,
                FollowOffset.lerpTime
            ).SetEase(FollowOffset.easeType);
        }

        // LookOffset
        if (LookOffset.isInable) {
            DOTween.To(
                () => composer.m_TrackedObjectOffset,
                x => composer.m_TrackedObjectOffset = x,
                LookOffset.toVal,
                LookOffset.lerpTime
            ).SetEase(LookOffset.easeType);
        }
    }

    private void Awake() {
        cam = CameraManager.Instance._curActiveCamera;
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer == null) {
            DebugLogger.Warning(this.name, "Didn't find Composer on CurrentVirtualCamera, FollowAtOffset Disabled.");
            FollowOffset.isInable = false;
        }

        composer = cam.GetCinemachineComponent<CinemachineComposer>();
        if(composer == null) {
            DebugLogger.Warning(this.name, "Didn't find Composer on CurrentVirtualCamera, LookAtOffset Disabled.");
            LookOffset.isInable = false;

        }
    }
}
