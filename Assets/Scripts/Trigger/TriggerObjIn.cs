using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerObjIn : TriggerBase
{
    public Transform obj;

    protected override void enterEvent()
    {
        Debug.Log(" innnnnn");
        base.enterEvent();
        obj.gameObject.SetActive(true);
    }
}