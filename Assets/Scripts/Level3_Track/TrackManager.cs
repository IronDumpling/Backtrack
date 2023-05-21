using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Common;
using System;
using DG.Tweening;

namespace Level3_Track {
    /// <summary>
    /// By using Current Track's setted Curve and Speed limitation, TrackManager controls
    /// Player's Dolly Cart Current Speed, and switch Path while current Path ends 
    /// </summary>
    public class TrackManager : MonoSingleton<TrackManager> {

        [Header("TrackManager Settings")]
        public Track[] _TrackList;
        public int _CurrentTrackIdx = 0;

        private CinemachineBrain _cmbrain;

        private PlayerController_L3 _playerController;
        private CinemachineDollyCart _playerDollyCart;

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
        public void TrackSwitch() {
            if(_CurrentTrackIdx >= _TrackList.Length) {
                Debug.Log("All Track Played successfully");
                Debug.Break();
                return;
            }

            Track curTrack = _TrackList[_CurrentTrackIdx];
            curTrack._TrackVirtualCamera.m_Priority = (_CurrentTrackIdx+1);

            // TODO: use to lerp to Transform Player Position to TrackStarting point
            Vector3 fromPos = _playerController.transform.position;
            Vector3 toPos = curTrack.transform.TransformPoint(curTrack._PlayerTrack.m_Waypoints[0].position);
            Debug.Log("from :" + fromPos);
            Debug.Log("to :" + toPos);
            _playerDollyCart.m_Path = null;
            DOTween.To(
                () => fromPos,
                x => _playerController.transform.position = fromPos = x,
                toPos,
                1f
            ).SetEase(DOTween.defaultEaseType)
            .onComplete += () => {
                _playerDollyCart.m_Path = curTrack._PlayerTrack;
                _playerDollyCart.m_Position = 0;
            };
            //_playerDollyCart.m_Position = 0;
            //_playerDollyCart.m_Path = curTrack._PlayerTrack;
            Quaternion toQT = curTrack._PlayerTrack.EvaluateOrientation(0);

            _playerController.transform.DORotate(toQT.eulerAngles, 1f);
            
            
            //curTrack._PlayerTrack.m_Waypoints[0].
            
            A_TrackUpdate = TrackNormal;
            StartCoroutine(CamBlendYieldPlyControl(curTrack));
        }

        private IEnumerator CamBlendYieldPlyControl(Track curTrack) {
            _playerController.GameEnd();
            do {
                yield return null;
            } while (_cmbrain.IsBlending);

            _playerController.GameStart();
            _playerController.SwitchMoveMapping(curTrack._InputMapping);
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
            _playerController = FindObjectOfType<PlayerController_L3>();
            if (_playerController == null) {
                DebugLogger.Error(this.name, "_playerController not Found!");
            }

            _playerDollyCart = _playerController.GetComponent<CinemachineDollyCart>();
            if (_playerDollyCart == null) {
                DebugLogger.Error(this.name, "_playerDollyCart not Found!");
            }

            _cmbrain = FindObjectOfType<CinemachineBrain>();
            if (_cmbrain == null) {
                Debug.LogError("CinemachineBrain not found in scene");
            }

            A_TrackUpdate = TrackSwitch;
        }


        private void Update() {
            A_TrackUpdate?.Invoke();
        }
    }
}