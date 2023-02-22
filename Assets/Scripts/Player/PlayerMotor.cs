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
        _nextMoveZVelocity = Vector3.forward * Zspeed * Time.deltaTime;
        
    }

    public void MoveHorizontal(float dir)
    {
        _nextMoveXVelocity = new Vector3(dir,0,0) * Xspeed * Time.deltaTime;

    }

    public void Move()
    {
        if (canMove)
        {
            _rigidBody.MovePosition(transform.position + _nextMoveXVelocity + _nextMoveZVelocity);
        }

    }

    void FixedUpdate()
    {
        var position = transform.position;
        var prePos = _prePosition;
        _currentYSpeed = (position.y - prePos.y) * speedConstant;
        position.y = 0;
        prePos.y = 0;
        _currentSpeed = (position - prePos).magnitude * speedConstant;
        _currentXSpeed = (position.x - _prePosition.x) * speedConstant;
        _prePosition = position;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,Vector3.down);
    }
}
