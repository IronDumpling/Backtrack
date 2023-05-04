using Common;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerController_L3: MonoSingleton<PlayerController_L3>
{
    private PlayerInput _playerInput;
    private InputAction _inputMove;
    private PlayerMotor_L3 _playerMotor;

    private Action<Vector2> A_planeMoveUpdate;

    #region Controll Motor Public Funcs
    public void SwitchMoveMapping(EInputMapping changeToMapping) {
        switch (changeToMapping) {
            case EInputMapping.DISABLE:
                A_planeMoveUpdate = null;
                break;
            case EInputMapping.TOPDOWN:
                A_planeMoveUpdate = _playerMotor.TopDownMove;
                break;
            case EInputMapping.EYELEVEL:
                A_planeMoveUpdate = _playerMotor.EyeLevelMove;
                break;
        }
    }

    //TODO: write Function to help Bird localPosition back to Zero
    #endregion

    private void OnEnable() {
        _inputMove = _playerInput.Player.Move;
        _playerInput.Enable();

        _playerMotor = GetComponent<PlayerMotor_L3>();
    }
    private void OnDisable() {
        _playerInput.Disable();
    }
    protected override void Init() {
        _playerInput = new PlayerInput();
    }

    private void Update() {
        A_planeMoveUpdate?.Invoke(_inputMove.ReadValue<Vector2>());
    }
}
