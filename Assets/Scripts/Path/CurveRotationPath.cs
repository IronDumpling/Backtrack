using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurveRotationPath : MonoBehaviour
{
    private Vector3 _startPoint;
    private Transform _startTr;
    private Vector3 _endPoint;
    private Vector3 _midPoint;
    [SerializeField] private float curveTurnSpeed = 40f;
    
    [SerializeField] private int segmentNum = 50;
    private Vector3[] _listPoint;

    private Vector3[] _listDirection;

    // public Vector3[] ListDirection
    // {
    //     get => _listDirection;
    // }
    //
    // public Vector3[] ListPoint
    // {
    //     get => _listPoint;
    // }
    //可以直接写成属性，用的时候再算
    private void Awake()
    {
        _startTr = transform.Find("StartPoint");
        _startPoint = _startTr.position;
        _endPoint = transform.Find("EndPoint").position;
        _midPoint = transform.Find("MidPoint").position;

        if (_startPoint == null || _endPoint == null || _midPoint == null)
        {
            Debug.LogError(transform.name + " 没有叫startPoint || endPoint || midPoint的子类， 无法生成贝塞尔曲线");
        }


        _listPoint = BezierUtils.GetBezierCubicList(_startPoint, _midPoint, _endPoint, segmentNum);
        
        
        //计算坐标之间的差值，算方向
        _listDirection = new Vector3[_listPoint.Length];
        for (var i = 0; i < _listPoint.Length - 1; i++)
        {
            _listDirection[i] = (_listPoint[i + 1] - _listPoint[i]).normalized;
        }

        _listDirection[_listPoint.Length - 1] = _listDirection[_listPoint.Length - 2];
    }

    private void OnDrawGizmos()
    {
        if (_listPoint == null) return;

        for (var i = 0; i < _listPoint.Length - 1; i++)
        { 
            Gizmos.color = Color.green; 
            Gizmos.DrawLine(_listPoint[i],_listPoint[i+1]);
        }
        for (var i = 0; i < _listPoint.Length - 1; i++)
        { 
            Gizmos.color = Color.green; 
            Gizmos.DrawLine(_listPoint[i] +dif,_listPoint[i+1] + dif);
        }
        

        if (_listDirection == null) return;
        for (var i = 0; i < _listDirection.Length - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_listPoint[i], _listDirection[i] * 5);
        }
    }

    private Vector3 dif = Vector3.zero;
    public void StartPointBlockTriggered(Collider col, Transform startBlockTr)
    {
        if (col.transform.CompareTag("Player"))
        {
            dif = col.transform.position - _startTr.position;
            col.transform.GetComponent<PlayerMotorNew>().addCurvePath(_listDirection, _listPoint, dif, curveTurnSpeed);
            //startBlockTr.gameObject.SetActive(false);
        }
    }
}
