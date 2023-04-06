using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TriggerObjectFadeIn : TriggerBase
{
    [Header("指定播放的Animator 如果为空则在当前物体搜animator")]
    [SerializeField] private Animator _animator;
    [SerializeField, ReadOnly] private String triggerName = "FadeInTrigger";

    private Renderer _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_animator == null) _animator = GetComponent<Animator>();

        _renderer.enabled = false;
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        FadeIn();
    }

    void FadeIn()
    {
        _renderer.enabled = true;
        _animator.SetTrigger(triggerName);
    }
}
