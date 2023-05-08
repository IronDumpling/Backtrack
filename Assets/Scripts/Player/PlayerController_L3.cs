using Common;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


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

    public override void GameEnd() {
        base.GameEnd();
        A_planeMoveUpdate = null;
        _playerMotor.MotorReset();
    }

    protected override void Init() {
        base.Init();
        _inputMove = _playerInput.Player.Move;

        _playerMotor = GetComponent<PlayerMotor_L3>();
    }

    private void Update() {
        A_planeMoveUpdate?.Invoke(_inputMove.ReadValue<Vector2>());
    }
}
