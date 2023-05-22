using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class TriggerObjectFadeIn : TriggerBase
{
    [Header("指定播放的Animator 如果为空则在当前物体搜animator")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;
    [SerializeField, ReadOnly] private String triggerName = "FadeInTrigger";
    [SerializeField] private bool isUsingAudioSource = false;
    [SerializeField] private bool isUsingAnimator = true;
    [SerializeField] private bool isUsingFadeIn = true;
    [SerializeField] private bool isUsingFadeOut = true;
    
    private List<Transform> _childList;
    private Renderer _renderer;
    
    private void Awake()
    {
        if (_animator == null && isUsingAnimator) _animator = GetComponent<Animator>();
        if (_audio == null && isUsingAudioSource) _audio = GetComponent<AudioSource>();

        _childList = new List<Transform>();
        _renderer = GetComponent<Renderer>();
        if (isUsingFadeIn && _renderer != null) _renderer.enabled = false;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childObj = transform.GetChild(i).GetComponent<Transform>();
            if (childObj == null) continue;
            if (isUsingFadeIn) childObj.gameObject.SetActive(false);
            _childList.Add(childObj);
        }
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        if (isUsingFadeIn) FadeIn();
        if (_animator != null && isUsingAnimator) _animator.SetTrigger(triggerName);
        if (_audio != null && isUsingAudioSource) _audio.Play();
    }

    void FadeIn()
    {
        foreach (var child in _childList)
        {
            child.gameObject.SetActive(true);
        }
        if (_renderer != null) _renderer.enabled = true;
    }

    protected override void ExitEvent()
    {
        base.ExitEvent();
        if(isUsingFadeOut) FadeOut();
    }

    void FadeOut()
    {
        foreach (var child in _childList)
        {
            child.gameObject.SetActive(false);
            if(_renderer != null) _renderer.enabled = true;
        }
    }
}
