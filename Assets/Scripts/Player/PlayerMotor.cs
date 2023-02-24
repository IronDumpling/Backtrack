using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.UIElements;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMotor : MonoBehaviour
{
    private float speedConstant = 100f;
    
    private Rigidbody _rigidBody;
    private Collider _collider;

    private Vector3 _nextMoveXVelocity;
    private Vector3 _nextMoveZVelocity;
    private bool canMove = false;
    #region 外部访问属性
    private float _currentSpeed;
    public float CurrentSpeed
    {
        get => _currentSpeed;
    }

    private float _currentXSpeed;

    public float CurrentXSpeed
    {
        get => _currentXSpeed;
    }

    private float _currentYSpeed;
    public float CurrentYSpeed
    {
        get => _currentXSpeed;
    }


    #endregion
    
    #region 人物属性
    [Header("人物移动属性")]
    [SerializeField] private float Zspeed = 1f;
    [SerializeField] private float Xspeed = 1f;
    // [SerializeField] private float XMaxSpeed = 2f;
    // [SerializeField] private float XAccSpeed = 1f;
    [SerializeField] private float jumpVerticalForce = 2f;
    #endregion
    [Header("碰撞/射线属性")] 
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float detectionLength = 0.2f;


    
    private Vector3 _prePosition;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        canMove = false;
        if(_rigidBody == null) Debug.LogError("未找到RigidBody");
        
        _prePosition = transform.position;
    }

    public void MoveForward()
    {
        _nextMoveZVelocity = transform.forward * Zspeed * Time.deltaTime;
        Debug.DrawLine(transform.position,transform.forward + transform.position);
    }

    public void MoveHorizontal(float dir)
    {
        Vector3 pos = Vector3.zero;
        if (dir > 0) pos = transform.right * dir;
        if (dir < 0) pos = transform.right * dir;
        _nextMoveXVelocity = pos * Xspeed * Time.deltaTime;

    }

    public void Move()
    {
        if (canMove)
        {
            Vector3 velBeforeSlope = (_nextMoveXVelocity + _nextMoveZVelocity) * speedConstant;

            Vector3 velAfterSlope = velBeforeSlope;
            RaycastHit hit;
            if (IsOnSlope(out hit))
            {
                Debug.Log("in");
                velAfterSlope = Vector3.ProjectOnPlane(velBeforeSlope, hit.normal);
            }

            _rigidBody.velocity = velAfterSlope;
            
        }

    }

    void FixedUpdate()
    {
        var locVel = transform.InverseTransformDirection(_rigidBody.velocity);
        _currentYSpeed = locVel.y;
        _currentXSpeed = locVel.x;
        _currentSpeed = locVel.z;

    }

    public void MotorStart()
    {
        canMove = true;
    }

    public void JumpVertical()
    {
        if (!canMove) return;
        if (IsGrounded())
        {
            _rigidBody.AddForce(Vector3.up * jumpVerticalForce * speedConstant);
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _collider.bounds.extents.y + 0.1f,groundLayer);
    }

    private bool IsOnSlope(out RaycastHit hit)
    {
        
        if (Physics.Raycast(
                transform.position, Vector3.down,  out hit,
                _collider.bounds.extents.y + detectionLength, groundLayer))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
}
