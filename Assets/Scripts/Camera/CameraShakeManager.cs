using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Cinemachine;
public class CameraShakeManager : MonoSingleton<CameraShakeManager>
{
    private CinemachineImpulseSource impulseSource;
    private CinemachineImpulseDefinition impulse;
    private CinemachineImpulseListener curListener;

    private void Awake() {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        impulse = impulseSource.m_ImpulseDefinition;
    }

    public void ScreenShakeFromProfile(ScreenShakeProfile prof) {
        // apply settings
        SetScreenShakeSettings(prof);

        // screenshake
        impulseSource.GenerateImpulseWithForce(prof.impactForce);
    }

    private void SetScreenShakeSettings(ScreenShakeProfile prof) {
        var trackManager = Level3_Track.TrackManager.Instance;
        curListener = trackManager._TrackList[trackManager._CurrentTrackIdx]._TrackVirtualCamera.GetComponent<CinemachineImpulseListener>();
        if(curListener == null) {
            DebugLogger.Log(this.name, "Current Active VC does not have ImpulseListener, please assign one!");
            return;
        }
        // Impulse Source Settings
        impulse.m_ImpulseDuration = prof.impactTime;
        impulse.m_CustomImpulseShape = prof.impluseCurve;
        impulseSource.m_DefaultVelocity = prof.defaultVelocity;

        // Impulse Listener Settings
        curListener.m_ReactionSettings.m_AmplitudeGain = prof.listenerAmplitude;
        curListener.m_ReactionSettings.m_FrequencyGain = prof.listenerFrequency;
        curListener.m_ReactionSettings.m_Duration = prof.listenerDuration;
    }
}
