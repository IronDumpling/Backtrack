using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerMovableCurvePath : TriggerBase
{
    [Header("向该方向转向移动多少度，y为左右转向； rotateDuration为转向速度")] 
    [Header("curve为起点到终点的进度函数，x轴为时间，y轴为位置，y轴可以超过1，代表超过想要移动的角度")]
    [SerializeField] private Vector3 finalRotation = new Vector3(0, 90, 0);
    [SerializeField] private float rotateDuration = 3f;
    [SerializeField] private AnimationCurve curve;

    private void Start()
    {
        isOneTime = true;
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        PlayerController.Instance.GetComponent<PlayerMotor>().RotateInSelfAxis(finalRotation, rotateDuration ,curve);
    }
}
