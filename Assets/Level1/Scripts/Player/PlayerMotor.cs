using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed;
    [SerializeField] private float curveTurnSpeed = 40f;

    private Queue<Vector3> _curveDirectionList;


    public Boolean isOnCurve
    {
        get => _curveDirectionList != null;
    }

    public Boolean isSameDirectionAsCurveList
    {
        get => Vector3.Distance(_curveDirectionList.Peek(), this.transform.forward) < 0.1f;
    }

    public Vector3 nextPointInCurveList
    {
        get
        {
            if (isSameDirectionAsCurveList) _curveDirectionList.Dequeue();
            return _curveDirectionList.Peek();
        } 
    }


    //test
    public Transform path;
    private void Start()
    {
        //_curveDirectionList = new Queue<Vector3>(path.GetComponent<CurveRotationPath>().ListDirection);
    }

    private float ttime = 0f;
    void Update()
    {
        //Debug.Log(transform.forward);
        MoveForward();
        ttime += Time.deltaTime;
        if (ttime >= 0.12f)
        {
            ttime = 0f;
            _curveDirectionList.Dequeue();
        }
        //public Transform lookAtTransform;
        //public float lookAtSpeed;
        //SlowLookAtRotate(lookAtTransform.position,lookAtSpeed);
        lookAtInCurve();
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime  * speed, Space.Self);
    }

    public void SlowLookAtRotate(Vector3 lookAtPoint, float lookAtSpeed)
    {
        Vector3 direction = lookAtPoint - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lookAtSpeed * Time.deltaTime);
    }
    
    
    //public void Slow
    public void lookAtInCurve()
    {
        if (!isOnCurve)
        {
            Debug.Log("不在弯道中");
            return;
        }
        
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (nextPointInCurveList), Time.deltaTime * curveTurnSpeed);
        
    }
    
    public void OnDisable() 
    {
        
    }
}
