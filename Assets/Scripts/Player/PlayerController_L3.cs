using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_L3: MonoSingleton<PlayerController_L3>
{
    private PlayerInput _playerInput;
    private InputAction _InputMove;
    

    protected override void Init() {
        _playerInput = new PlayerInput();
    }

    private void OnEnable() {
        _InputMove = _playerInput.Player.Move;
        _playerInput.Enable();
    }
    private void OnDisable() {
        _playerInput.Disable();
    }

    private void Update() {
        _InputMove.ReadValue<Vector2>();
    }
}
