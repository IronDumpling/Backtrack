using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBase : MonoBehaviour
{
    [SerializeField] public LayerMask targetLayer;//触发器的目标层
    [HideInInspector]
    public bool canWork = true;
    [HideInInspector]
    public bool isOneTime = false;

    protected virtual void OnTriggerEnter(Collider collision)//触发器进入事件
    {
        if (canWork &&  targetLayer == (targetLayer | (1 << collision.gameObject.layer)) )
        {
            enterEvent();

        }
    }

    
    protected virtual void enterEvent()
    {

    }


    protected virtual void OnTriggerExit(Collider collision)
    {
    
        if (canWork && targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            exitEvent();
            if(isOneTime)
                canWork = false;
        }
    }

    protected virtual void exitEvent()
    {

    }

}