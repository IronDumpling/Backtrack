using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraDollyTrack_L3 : MonoBehaviour {
    public float _TrackCamSpeed = 8f;
    public Ease _EaseType = DOTween.defaultEaseType;
    public CinemachineSmoothPath _DollyTrackPath;

    private CinemachineTrackedDolly _trackVC;
    private CinemachineVirtualCamera _previousVC;

    private void OnEnable() {
        _previousVC = CameraManager.Instance._curActiveCamera;

        PlayerLookAt.Instance._IsFixOnPath = false;

        _trackVC.m_Path = _DollyTrackPath;
        _trackVC.m_PathPosition = 0f;
        _trackVC.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;

        float pathLength = _DollyTrackPath.PathLength;

        DOTween.To(
        () => _trackVC.m_PathPosition,
        x => _trackVC.m_PathPosition = x,
        pathLength,
        pathLength / _TrackCamSpeed).SetEase(_EaseType)
        .onComplete += () => {
            CameraManager.Instance.SwitchCamera(_previousVC);
            PlayerLookAt.Instance._IsFixOnPath = true;
        };
    }

    private void Awake() {
    }
}
