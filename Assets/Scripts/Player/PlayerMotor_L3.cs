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

    public float EdgeBoundaryOffsetXPixel = 50f;
    public float EdgeBoundaryOffsetYPixel = 50f;

    #region Check Boundary Func
    // ruff bounding
    private void filterBoundClamp(ref Vector2 input) {
        Camera cam = Camera.main;

        Vector3 ScreenPos = cam.WorldToScreenPoint(_rigidBody.transform.position);

        if (ScreenPos.x > cam.pixelWidth - EdgeBoundaryOffsetXPixel) {
            input.x = Mathf.Clamp(input.x, Mathf.NegativeInfinity, 0f);
            Debug.Log("Right");
        }
        else if (ScreenPos.x <= 0f) {
            Debug.Log("Left");
            input.x = Mathf.Clamp(input.x, 0f, Mathf.Infinity);
        }

        if (ScreenPos.y > cam.pixelHeight - EdgeBoundaryOffsetYPixel) {
            Debug.Log("Up");
            input.y = Mathf.Clamp(input.y, Mathf.NegativeInfinity, 0f);
        }
        else if (ScreenPos.y <= 0f) {
            Debug.Log("Down");
            input.y = Mathf.Clamp(input.y, 0f, Mathf.Infinity);
        }
    }

    private bool checkInBoundary(float x, float y, float width, float height) {
        if (x < EdgeBoundaryOffsetXPixel || y < EdgeBoundaryOffsetYPixel || x > width - EdgeBoundaryOffsetXPixel || y > height - EdgeBoundaryOffsetYPixel) 
            return false;
        return true;
    }

    // percise bounding
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
    #endregion

    #region Motion Update Func
    public void TopDownMove(Vector2 input) {

        Vector3 worldX = this.transform.right;
        Vector3 worldY = this.transform.forward;

        //filterBound(ref input, worldX * input.x * 0.1f, worldY * input.y * 0.1f);
        filterBoundClamp(ref input);

        Vector3 worldDirection = input.x * worldX + input.y * worldY;
        _rigidBody.velocity = worldDirection * _SpeedCoeff;
    }

    public void EyeLevelMove(Vector2 input) {
        
        Vector3 worldX = this.transform.right;
        Vector3 worldY = this.transform.up;

        filterBoundClamp(ref input);

        Vector3 worldDirection = input.x * worldX + input.y * worldY;
        _rigidBody.velocity = worldDirection * _SpeedCoeff;
    }

    public void SideViewMove(Vector2 input) {
        Vector3 worldY = this.transform.up;

        filterBoundClamp(ref input);

        Vector3 worldDirection = input.y * worldY;
        _rigidBody.velocity = worldDirection * _SpeedCoeff;
    }
    #endregion

    public void MotorReset() {
        _rigidBody.velocity = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(0, 0, 0);
    }

    private void Awake()
    {
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }
}
