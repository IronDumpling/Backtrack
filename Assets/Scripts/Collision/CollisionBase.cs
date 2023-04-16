using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CollisionBase : MonoBehaviour
{
    [Header("为了解决勾选convex后不能用isTrigger的问题，该物体需要有rigidbody，而且要把rigidbody设置成isKinetic")]
    [SerializeField] public LayerMask targetLayer;//触发器的目标层
    [HideInInspector]
    public bool canWork = true;
    [HideInInspector]
    public bool isOneTime = false;

    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (canWork &&  targetLayer == (targetLayer | (1 << collision.gameObject.layer)) )
        {
            EnterEvent();
            EnterEvent(collision);
        }
    }

    protected virtual void EnterEvent()
    {

    }
    protected virtual void EnterEvent(Collision collision)
    {
        
    }

    
    protected virtual void OnCollisionStay(Collision collision)//触发器进入事件
    {
        if (canWork && targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            StayEvent();
            StayEvent(collision);
        }
    }

    protected virtual void StayEvent()
    {
        
    }

    protected virtual void StayEvent(Collision collision)
    {
        
    }
    
    protected virtual void OnCollisionExit(Collision collision)
    {
    
        if (canWork && targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            ExitEvent();
            if(isOneTime)
                canWork = false;
        }
    }

    protected virtual void ExitEvent()
    {

    }
}
