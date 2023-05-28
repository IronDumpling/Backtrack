using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScreenShake/New Profile")]
public class ScreenShakeProfile : ScriptableObject {
    [Header("Impulse Source Settinggs")]
    public float impactTime = 0.2f;
    public float impactForce = 1f;
    public Vector3 defaultVelocity = new Vector3(1f, 1f, 1f);
    public AnimationCurve impluseCurve;

    [Header("Impulse Listener Settings")]
    public float listenerAmplitude = 1f;
    public float listenerFrequency = 1f;
    public float listenerDuration = 1f;

} 
