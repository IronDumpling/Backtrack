using System;
using System.Collections;
using System.Numerics;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.UIElements;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMotor : MonoBehaviour
{
    private float speedConstant = 100f;
    
    private Rigidbody _rigidBody;
    private Collider _collider;

    private Vector3 _nextMoveXVelocity;
    private Vector3 _nextMoveZVelocity;
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
    #region 碰撞/射线属性
    [Header("碰撞/射线属性")] 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float detectionLength = 0.2f;
    #endregion
    
    #region 布尔变量，motor状态
    private bool canMove = false; //是否可以移动（包括跳跃） 移动的总控制
    private bool canMoveRight = true; //是否可以像左移动
    private bool canMoveLeft = true; //是否可以像右移动
    #endregion
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        canMove = false;
        if(_rigidBody == null) Debug.LogError("未找到RigidBody");
        
    }

    public void MoveForward()
    {
        _nextMoveZVelocity = transform.forward * Zspeed * Time.deltaTime;
        Debug.DrawLine(transform.position,transform.forward + transform.position);
    }

    public void MoveHorizontal(float dir)
    {
        Vector3 pos;
        //检测是否可以往左走，是否可以往右走
        if (!canMoveRight && dir > 0)
        {
            pos = Vector3.zero;
            
        }

        if (!canMoveLeft && dir < 0)
        {
            pos = Vector3.zero;

        }
        else
        {
           pos  =  transform.right * dir;

        }
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
                velAfterSlope = Vector3.ProjectOnPlane(velBeforeSlope, hit.normal);
            }
            else
            {
                velAfterSlope.y = _rigidBody.velocity.y;
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

        RotateInFixedUpdate();
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

    private UnityEngine.Quaternion rotate_StartPos;
    private UnityEngine.Quaternion rotate_EndPos;
    private float rotate_duration;
    private float rotate_addUpTime;
    private AnimationCurve rotate_curve;
    private bool rotate_start = false;
    public void RotateInSelfAxis(Vector3 rotateAngle, float duration,AnimationCurve curve)
    {
        rotate_StartPos = transform.rotation;
        rotate_EndPos = UnityEngine.Quaternion.Euler(rotate_StartPos.eulerAngles + rotateAngle);
        rotate_duration = duration;
        rotate_addUpTime = 0;
        rotate_start = true;
        rotate_curve = curve;
        // UnityEngine.Quaternion qt = UnityEngine.Quaternion.Slerp(rotate_StartPos, UnityEngine.Quaternion.Euler(rotate_StartPos.eulerAngles + rotateAngle), 0.1f);
        // transform.Rotate(qt.eulerAngles);
    }

    private void RotateInFixedUpdate()
    {

        if (!rotate_start) return;
        rotate_addUpTime += Time.deltaTime / rotate_duration;
        if (rotate_addUpTime > 1f)
        {
            rotate_start = false;
            return;
        }

        UnityEngine.Quaternion qt =
            UnityEngine.Quaternion.SlerpUnclamped(rotate_StartPos, rotate_EndPos, rotate_curve.Evaluate(rotate_addUpTime));
        transform.Rotate(qt.eulerAngles - transform.rotation.eulerAngles);
        if (rotate_addUpTime > 1f) rotate_start = false;

    }

    public void DisableMoveLeft(bool disable)
    {
        if (disable)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }
    }

    public void DisableMoveRight(bool disable)
    {
        if (disable)
        {
            canMoveRight = false;

        }
        else
        {
            canMoveRight = true;
        }
    }

    
}
