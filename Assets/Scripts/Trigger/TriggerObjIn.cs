using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerObjIn : TriggerBase
{
    public Transform obj;

    private void Start()
    {
        obj.gameObject.SetActive(false);
    }

    protected override void enterEvent()
    {
        
        base.enterEvent();
        obj.gameObject.SetActive(true);
    }
}