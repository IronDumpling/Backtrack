using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoSingleton<PlayerController>
{

    
    
    
    
    
    
    
    
    
    
    //玩家输入
    private PlayerInput _playerInput;
    private InputAction _inputXMovement; //玩家AD左右输入
    //玩家引擎(控制移动)
    private PlayerMotor _motor;
    //玩家动画控制器
    private PlayerAnimatorController _animController;
    
    protected override void Init()
    {
        _playerInput = new PlayerInput();
        _motor = GetComponent<PlayerMotor>();
        _animController = GetComponent<PlayerAnimatorController>();
    }

    private void OnEnable()
    {
        //玩家移动时
        _inputXMovement = _playerInput.Player.Move;
        _inputXMovement.performed += InputXMovementOnperformed;
        //玩家点击开火按钮
        _playerInput.Player.Fire.performed += FireOnperformed;
        
        
        
        _playerInput.Enable();
    }

    private void FireOnperformed(InputAction.CallbackContext obj)
    {
        //TODO: 先写成按左键开始游戏，后面设计成完成开始动画开始游戏（玩家移动）
        _motor.MotorStart();
        
    }

    private void InputXMovementOnperformed(InputAction.CallbackContext obj)
    {
        //设置Animator
        //obj.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        //移动
        _motor.MoveForward();
        _motor.MoveHorizontal(_inputXMovement.ReadValue<Vector2>().x);
        _motor.Move();
        
        _animController.SetFloat(_animController.animParam_Speed ,_motor.CurrentSpeed);
        _animController.SetFloat(_animController.animParam_XSpeed,_motor.CurrentXSpeed);
    }
    


}
