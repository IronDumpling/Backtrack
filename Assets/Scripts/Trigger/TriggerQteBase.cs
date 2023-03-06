using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TriggerQteBase : TriggerBase
{
    [SerializeField] private float timeScale = 0.2f;
    [SerializeField] private float timeScaleChangeDuration = 1f;
    [SerializeField] private Ease _easeType = DOTween.defaultEaseType;
    
    [SerializeField] private Transform qteUI;

    [SerializeField] private float qteShowDuration = 4f;

    private Tween slowTimeScaleTween = null;//缓慢变化时间缩放
    private void Awake()
    {
        isOneTime = true;
        
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        slowTimeScaleTween = DOTween.To(()=>Time.timeScale,
            x=> Time.timeScale= x,
            timeScale,timeScaleChangeDuration).SetEase(_easeType);
        StartCoroutine(nameof(showQTE));//显示QTE
    }

    IEnumerator showQTE()
    {
        qteUI.gameObject.SetActive(true);
        //TODO: trigger an event 
        PlayerController.Instance._PlayerInput.Player.QTEKey1Press.performed += QTEKey1PressOnperformed;

        yield return new WaitForSecondsRealtime(qteShowDuration);
        Debug.Log("in coroutine");
        PlayerController.Instance._PlayerInput.Player.QTEKey1Press.performed -= QTEKey1PressOnperformed;
        OnKeyPressedComplete();
    }

    private void QTEKey1PressOnperformed(InputAction.CallbackContext obj)
    {
        qteUI.GetComponent<Button>().animator.SetTrigger("Pressed");
        StopCoroutine(nameof(showQTE));
        if (slowTimeScaleTween != null)//如果还在缓慢变化时间缩放
        {
            if(slowTimeScaleTween.IsPlaying()) slowTimeScaleTween.Kill();
        }
        Time.timeScale = 1f;
        PlayerController.Instance._PlayerInput.Player.QTEKey1Press.performed -= QTEKey1PressOnperformed;
        
        //TODO: 执行一个事情,以Action形式暴露接口
        PlayerController.Instance._motor.JumpVertical();
    }


    public void OnKeyPressedComplete()
    {
        qteUI.GetComponent<Button>().animator.SetTrigger("Disabled");
        Time.timeScale = 1f;
    }
    
}
