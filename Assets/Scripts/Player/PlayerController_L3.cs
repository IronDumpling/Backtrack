using Common;
using UnityEngine.InputSystem;
using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class PlayerController_L3: PlayerControllerBase
{
    private InputAction _inputMove;
    private PlayerMotor_L3 _playerMotor;

    private Action<Vector2> A_planeMoveUpdate;
    
    

    #region Controll Motor Public Funcs
    public void SwitchMoveMapping(EInputMapping changeToMapping) {

        switch (changeToMapping) {
            case EInputMapping.DISABLE:
                A_planeMoveUpdate = null;
                return;
            case EInputMapping.TOPDOWN:
                A_planeMoveUpdate = _playerMotor.TopDownMove;
                break;
            case EInputMapping.EYELEVEL:
                A_planeMoveUpdate = _playerMotor.EyeLevelMove;
                break;
            case EInputMapping.SIDEVIEW:
                A_planeMoveUpdate = _playerMotor.SideViewMove;
                break;
            case EInputMapping.EYELEVEL_X:
                A_planeMoveUpdate = _playerMotor.EyeLevelMoveOnlyX;
                break;
            case EInputMapping.EYELEVEL_Y:
                A_planeMoveUpdate = _playerMotor.EyeLevelMoveY;
                break;
        }
    }
    #endregion

    // private void OnEnable() {
    //     GameStart();
    // }
    private void OnDisable() {
        GameEnd();
    }

    // public void GameStart() {
    //     
    // }

    public override void GameEndLv3() {
        base.GameEndLv3();
        A_planeMoveUpdate = null;
        _playerMotor.MotorReset();
    }

    protected override void Init() {
        base.Init();
        _inputMove = _playerInput.Player.Move;

        _playerMotor = GetComponent<PlayerMotor_L3>();
    }

    public Transform birdslot;
    public override void GameEnd()
    {
        birdslot.GetComponent<Collider>().enabled = false;
        _playerMotor.enabled = false;
    }

 
    private void FixedUpdate()
    {
        Vector2 inputVec = _inputMove.ReadValue<Vector2>();
        
        A_planeMoveUpdate?.Invoke(inputVec);
        
        
    }
    
}
