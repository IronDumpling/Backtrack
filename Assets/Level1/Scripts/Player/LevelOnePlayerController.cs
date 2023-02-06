using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOnePlayerController : MonoBehaviour
{
    private Level1PlayerAction _actionLevel1;
    private PlayerMotorNew _motor;
    
    private InputAction _inputMovement;
    
    
    private void Awake()
    {
        _actionLevel1 = new Level1PlayerAction();
        
    }

    private void OnEnable()
    {
        _inputMovement = _actionLevel1.Player.Move;
        _inputMovement.Enable();

        _motor = GetComponent<PlayerMotorNew>();
    }

    private void FixedUpdate()
    {
        _motor.MoveHorizontal(_inputMovement.ReadValue<Vector2>().x);


        
    }


    
}
