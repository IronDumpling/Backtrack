using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Common;
using System;
namespace Level3_Track {
    /// <summary>
    /// By using Current Track's setted Curve and Speed limitation, TrackManager controls
    /// Player's Dolly Cart Current Speed, and switch Path while current Path ends 
    /// </summary>
    public class TrackManager : MonoSingleton<TrackManager> {

        [Header("TrackManager Settings")]
        public Track[] _TrackList;
        public int _CurrentTrackIdx = 0;

        private CinemachineDollyCart _playerDollyCart;
        private Transform _playerTransform;

        private Action A_TrackUpdate;

        private void TrackNormal() {
            if (_playerDollyCart.m_Path.PathLength == _playerDollyCart.m_Position) {
                _CurrentTrackIdx++;

                /* Update TrackSwitch */
                A_TrackUpdate = TrackSwitch;
                return;
            }
            Track curTrack = _TrackList[_CurrentTrackIdx];
            float t = _playerDollyCart.m_Position / _playerDollyCart.m_Path.PathLength;

            _playerDollyCart.m_Speed = curTrack._SpeedOffset + curTrack._SpeedAmplitude * curTrack._SpeedCurve.Evaluate(t);
        }
        /// <summary>
        /// 1. Enable Virtual Camera on Track<br/>
        /// 2. TODO: lerp player position to the starting point of next Track Starting Point<br/>
        /// 3. Use TrackInfo to Set Player's DollyCart<br/>
        /// 4. Switch back to TrackNormal Update<br/>
        /// 5. Use TrackInfo to Set PlayerMotor Input Mapping
        /// </summary>
        private void TrackSwitch() {
            if(_CurrentTrackIdx >= _TrackList.Length) {
                Debug.Log("All Track Played successfully");
                Debug.Break();
                return;
            }

            Track curTrack = _TrackList[_CurrentTrackIdx];

            // TODO: use to lerp to TrackStarting point
            // _playerDollyCart.m_Path = null;
            // move _playerTransform.position -> curTrack._PlayerTrack.m_Waypoints[0].position;


            curTrack._TrackVirtualCamera.m_Priority = (_CurrentTrackIdx+1);
            
            _playerDollyCart.m_Path = curTrack._PlayerTrack;
            _playerDollyCart.m_Position = 0;


            A_TrackUpdate = TrackNormal;
            PlayerController_L3.Instance.SwitchMoveMapping(curTrack._InputMapping);
        }


        private void Awake() {
            if (_TrackList == null || _TrackList.Length == 0) {
                DebugLogger.Error(this.name, "TrackList Empty! Please Set in Editor.");
            }
            if (_CurrentTrackIdx < 0 || _CurrentTrackIdx >= _TrackList.Length) {
                DebugLogger.Error(this.name, "_CurrentTrack Out Of Rangce! Please Set a Correct number in Editor.");
            }
        }

        private void Start() {
            _playerDollyCart = PlayerController_L3.Instance.GetComponent<CinemachineDollyCart>();
            _playerTransform = PlayerController_L3.Instance.GetComponent<Transform>();
            if (_playerDollyCart == null) {
                DebugLogger.Error(this.name, "_playerDollyCart not Found!");
            }
            if (_playerTransform == null) {
                DebugLogger.Error(this.name, "_playerTransform not Found!");
            }

            A_TrackUpdate = TrackSwitch;
        }

        private void Update() {
            A_TrackUpdate?.Invoke();
        }
    }
}