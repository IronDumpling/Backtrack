using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMotorNew : MonoBehaviour
{
    public float speed;
    [SerializeField] private float curveTurnSpeed = 40f;
    [SerializeField] private float horizontalCurveSpeed = 1f;
    private Queue<Vector3> _curvePointList;
    private Queue<Vector3> _curveDirectionList;
    private Vector3[] _curvePointArr;

    private Vector3 _posDifCprCurve = Vector3.zero; 
    public Boolean IsOnCurve
    {
        get
        {
            if (_curvePointList == null) return false;
            if (_curvePointList.Count <= 0) return false;
            return true;
        }
    }

    public Boolean IsSamePositionAsCurveList
    {
        get
        {
            Vector3 destination = FirstPointPlusDif;
            destination.y = 0;
            Vector3 transformPos = this.transform.position;
            transformPos.y = 0;
            return Vector3.Distance(destination, transformPos) < 0.05f;
            
        } 
    }

    public Vector3 FirstPointPlusDif
    {
        get
        {
            return _curvePointList.Peek() + _posDifCprCurve;
        }
    }
    
    public Vector3 NextPointInCurveList
    {
        get
        {
            if (IsSamePositionAsCurveList)
            {
                Vector3 deque = FirstPointPlusDif;
                _curvePointList.Dequeue();
                if(_curveDirectionList.Count > 0) _curveDirectionList.Dequeue();
                if (_curvePointList.Count <= 0) return deque;
            }
 
            return FirstPointPlusDif ;
            

        } 
    }

    public Vector3 NextDirectionInCurveList
    {
        get
        {
            if (_curveDirectionList.Count <= 0 || _curveDirectionList.Peek().Equals(Vector3.zero)) return transform.rotation.eulerAngles; 
            return _curveDirectionList.Peek();
        } 
    }


    public void MoveHorizontal(float x)
    {
        if (IsOnCurve)
        {
            _posDifCprCurve += transform.right * x * Time.deltaTime * horizontalCurveSpeed;
            //修改横向位置时，增加横向的移动速度，因为横向速度太快，往前的速度会为0
            transform.Translate(transform.right * x * Time.deltaTime * horizontalCurveSpeed);
        }
        else
        {
            transform.Translate(Vector3.right * x * Time.deltaTime  * speed, Space.Self);
        }

    }

    //test
    public Transform path;

    //由Path传入 directionlist 和 pointlist
    public void addCurvePath(Vector3[] dirList, Vector3[] pointList, Vector3 dif, float turnSpeed)
    {
        _curveDirectionList = new Queue<Vector3>(dirList);
        _curvePointList = new Queue<Vector3>(pointList);
        _posDifCprCurve = dif;
        curveTurnSpeed = turnSpeed;
    }
    
    void FixedUpdate()
    {
        if (IsOnCurve)
        {
            MoveForwardInCurve();
            rotateInCurve();
            //Debug.Log("in curve");
        }
        else
        {
            MoveForward();
            //Debug.Log("not in curve");

        }

  
        
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

    public void MoveForwardInCurve()
    {
        if (_curvePointList.Count <= 0) return;
        //不考虑y
        Vector3 nextPos = NextPointInCurveList;
        Vector3 transPos = transform.position;
        nextPos.y = 0;
        transPos.y = 0;
        var dir = (nextPos - transPos).normalized;
        // Then add the direction * the speed to the current position:
        Debug.Log("dir " + dir);
        transform.position += dir * speed * Time.deltaTime;
    }
    
    //public void Slow
    //TODO: rotate when move in curve
    public void rotateInCurve()
    {
        if (_curveDirectionList.Count <= 0) return;
        Vector3 dir = NextDirectionInCurveList;

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * curveTurnSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //
        if (_curvePointList != null && _curvePointList.Count != 0)
        {
            Gizmos.DrawLine(transform.position, _curvePointList.Peek());

        }
    }
}
