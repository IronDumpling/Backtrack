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
    private List<Transform> _childList;

    // private Renderer _renderer;
    
    private void Awake()
    {
        if (_animator == null) _animator = GetComponent<Animator>();

        _childList = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childObj = transform.GetChild(i).GetComponent<Transform>();
            if (childObj == null) continue;
            childObj.gameObject.SetActive(false);
            _childList.Add(childObj);
        }
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        FadeIn();
    }

    void FadeIn()
    {
        foreach (var child in _childList)
        {
            child.gameObject.SetActive(true);
        }

        _animator.SetTrigger(triggerName);
    }
}
