using System.Collections;
using UnityEngine;
using Cinemachine;


namespace Level3_Track {
    public class Track : MonoBehaviour {
        [Header("Track Generic Settings")]
        public CinemachineVirtualCamera _TrackVirtualCamera;
        public EInputMapping _InputMapping;
        public CinemachineSmoothPath _PlayerTrack;

        [Header("Track Speed Settings")]
        public AnimationCurve _SpeedCurve;
        [Tooltip("Curve t=0 and _SpeedOffset cannot be zero at the sametime")]
        public float _SpeedOffset;
        public float _SpeedAmplitude;

        private void Awake() {
            if (_TrackVirtualCamera == null) {
                DebugLogger.Error(this.name, "m_TrackVirtualCamera Not Found! Please Set in Editor.");
            }
            if(_PlayerTrack == null) {
                DebugLogger.Error(this.name, "_PlayerTrack Not Found! Please Set in Editor.");
            }
        }
    }
}