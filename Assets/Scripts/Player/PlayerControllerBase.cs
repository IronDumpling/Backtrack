using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayerControllerBase : MonoSingleton<PlayerControllerBase>
{
    protected PlayerInput _playerInput;

    protected override void Init()
    {
        _playerInput = new PlayerInput();

    }
    protected virtual void Start()
    {
        GameStart();
        
    }

    protected virtual void OnEnable()
    {
        _playerInput.UI.Pause.performed += UIManager.Instance.PausePreform;
        _playerInput.Enable();

    }


    public virtual void GameStart()
    {
        AudioManager.Instance.PlayMusicAtStart();
    }

    public virtual void GameEnd()
    {
       // _playerInput.UI.Pause.performed -= UIManager.Instance.PausePreform;

    }
    
    
}
