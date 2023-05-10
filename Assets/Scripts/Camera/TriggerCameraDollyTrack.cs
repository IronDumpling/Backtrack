using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class TriggerCameraDollyTrack : TriggerCamera {
    [SerializeField] private float trackCamSpeed = 8f;
    [SerializeField] private Ease easeType = DOTween.defaultEaseType;
    public CinemachinePathBase path;

    protected override void _awake() {
        base._awake();
        //_CustomVC
    }
}
