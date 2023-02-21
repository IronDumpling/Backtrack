using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBase : MonoBehaviour
{
    [SerializeField] public LayerMask targetLayer;
    [HideInInspector]
    public bool canWork = true;
    [HideInInspector]
    public bool isOneTime = false;

    protected virtual void OnTriggerEnter(Collider collision)
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