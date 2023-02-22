using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerCurvePath : TriggerBase
{
    
    [Header("放入点，生成曲线，让玩家以曲线模式移动，当前玩家位置为起点位置，最后一个点为终点位置")]
    [SerializeField] private Transform[] points;

    private Vector3[] pointPos;
    [SerializeField] private float speed;
    [SerializeField] private PathType type  = PathType.CatmullRom;
    private void Start()
    {
        if(points.Length == 0) Debug.LogError(transform.name + " 未加入点，生成不了曲线");
        pointPos = new Vector3[points.Length];
        isOneTime = true;
        for (var i = 0; i < points.Length; i++)
        {
            pointPos[i] = points[i].position;
        }
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        PlayerController.Instance.GetComponent<PlayerMotor>().enabled = false;
        PlayerController.Instance.transform.DOPath(pointPos, speed,type).SetSpeedBased(true).SetLookAt(0.01f).SetEase(Ease.Linear).onComplete = () =>
        {
            PlayerController.Instance.GetComponent<PlayerMotor>().enabled = true;
        };
    }
}