using System;
using UnityEngine;

public enum EInputMapping {
    DISABLE,
    TOPDOWN,
    EYELEVEL,
    SIDEVIEW
}

public class PlayerMotor_L3 : MonoBehaviour
{
    [Range(0.2f, 10)]
    public float _SpeedCoeff = 1f;

    private Rigidbody _rigidBody;

    public void MotorReset() {
        _rigidBody.velocity = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(0, 0, 0);
    }

    public void TopDownMove(Vector2 input) {
        Vector3 worldVelocity = transform.TransformVector(
            new Vector3(input.x, 0f, input.y) * _SpeedCoeff
        );
        _rigidBody.velocity = worldVelocity;
    }

    public void EyeLevelMove(Vector2 input) {
        Vector3 worldVelocity = transform.TransformVector(
            new Vector3(input.x, input.y, 0f) * _SpeedCoeff
        );
        _rigidBody.velocity = worldVelocity;
    }

    public void SideViewMove(Vector2 input) {
        Vector3 worldVelocity = transform.TransformVector(
            new Vector3(0f, input.y, 0f) * _SpeedCoeff
        );
        _rigidBody.velocity = worldVelocity;
    }

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }
}
