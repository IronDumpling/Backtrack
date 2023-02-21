using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private float speedConstant = 100f;
    
    private Rigidbody _rigidBody;

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
    

    #endregion
    
    #region 人物属性
    [Header("人物移动属性")]
    [SerializeField] private float Zspeed = 1f;
    [SerializeField] private float Xspeed = 1f;
    // [SerializeField] private float XMaxSpeed = 2f;
    // [SerializeField] private float XAccSpeed = 1f;

    #endregion
    
    
    private Vector3 _prePosition;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
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
        
        _currentSpeed = (position - _prePosition).magnitude * speedConstant;
        _currentXSpeed = (position.x - _prePosition.x) * speedConstant;
        _prePosition = position;
    }

    public void MotorStart()
    {
        canMove = true;
    }
}
