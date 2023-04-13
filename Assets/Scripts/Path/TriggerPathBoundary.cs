using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class TriggerPathBoundary : TriggerBase
{
    [Header("该物体是左边边界还是右边边界"),SerializeField] private bool isDetectingLeft;
    private PlayerMotor motor;
    private void Start()
    {
        motor = PlayerController.Instance.GetComponent<PlayerMotor>();
    }

    //protected override void enterEvent()
    //{
    //    Debug.Log("in");
    //    base.enterEvent();
    //    if (isDetectingLeft)
    //    {
    //        motor.DisableMoveLeft(true);
    //    }
    //    else
    //    {
    //        motor.DisableMoveRight(true);
    //    }
    //}

    protected override void stayEvent()
    {
        Debug.Log("stay");
        base.stayEvent();

        if (isDetectingLeft)
        {
            motor.DisableMoveLeft(true);
        }
        else
        {
            motor.DisableMoveRight(true);
        }

    }

    protected override void exitEvent()
    {
        Debug.Log("out");

        base.exitEvent();
        if (isDetectingLeft)
        {
            motor.DisableMoveLeft(false);
        }
        else
        {
            motor.DisableMoveRight(false);
        }
    
    }

}
