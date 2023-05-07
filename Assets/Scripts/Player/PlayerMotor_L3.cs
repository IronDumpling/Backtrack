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

    public float EdgeBoundaryOffsetX = 10f;
    public float EdgeBoundaryOffsetY = 5f;

    private bool checkInBoundary(float x, float y, float width, float height) {
        if (x < EdgeBoundaryOffsetX || y < EdgeBoundaryOffsetY || x > width - EdgeBoundaryOffsetX || y > height - EdgeBoundaryOffsetY) 
            return false;
        return true;
    }

    private void filterBoundClamp(ref Vector2 input) {
        Camera cam = Camera.main;

        Vector3 ScreenPosX = cam.WorldToScreenPoint(_rigidBody.transform.position);

        
    }

    private void filterBound(ref Vector2 input, Vector3 stepX, Vector3 stepY) {
        Camera cam = Camera.main;

        Vector3 ScreenPosX = cam.WorldToScreenPoint(_rigidBody.transform.position + stepX);
        Vector3 ScreenPosY = cam.WorldToScreenPoint(_rigidBody.transform.position + stepY);

        if (!checkInBoundary(ScreenPosX.x, ScreenPosX.y, cam.pixelWidth, cam.pixelHeight)) {
            input.x = 0f;
            _rigidBody.AddForce(this.transform.position - _rigidBody.transform.position, ForceMode.Impulse);
        }
        if (!checkInBoundary(ScreenPosY.x, ScreenPosY.y, cam.pixelWidth, cam.pixelHeight)) {
            input.y = 0f;
            _rigidBody.AddForce(this.transform.position - _rigidBody.transform.position, ForceMode.Impulse);

        }
    }

    public void TopDownMove(Vector2 input) {

        Vector3 worldX = this.transform.right;
        Vector3 worldY = this.transform.forward;
            

        filterBound(ref input, worldX * input.x * 0.1f, worldY * input.y * 0.1f);

        Vector3 worldDirection = input.x * worldX + input.y * worldY;
            
        _rigidBody.velocity = worldDirection * _SpeedCoeff;

        
    }

    public void EyeLevelMove(Vector2 input) {
        //filterBound(ref input);

        Vector3 worldVelocity = transform.TransformVector(
            new Vector3(input.x, input.y, 0f) * _SpeedCoeff
        );
        _rigidBody.velocity = worldVelocity;
    }

    public void SideViewMove(Vector2 input) {
        //filterBound(ref input);

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
