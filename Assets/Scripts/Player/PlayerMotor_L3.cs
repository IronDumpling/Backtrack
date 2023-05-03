using System;
using UnityEngine;

public enum EInputMapping {
    DISABLE,
    TOPDOWN,
    EYELEVEL,
}

public class PlayerMotor_L3 : MonoBehaviour
{
    [Range(0.2f, 10)]
    public float _SpeedCoeff = 1f;


    private Rigidbody _rigidBody;


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

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }
}
