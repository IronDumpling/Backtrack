using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class TriggerPathBoundary : TriggerBase
{
    [Header("该物体是左边边界还是右边边界"),SerializeField] private bool isDetectingLeft;
    private PlayerMotor motor;

    private void Awake()
    {
        if (!GameBuildSettingManager.Instance.isActivateTriggerPathBoundary)
        {
            this.gameObject.SetActive(false);
        }
        
    }

    private void Start()
    {
        motor = PlayerController.Instance.GetComponent<PlayerMotor>();
    }


    protected override void StayEvent(Collider collision)
    {
        base.StayEvent();
        //找到
        Vector3 collisionPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        Vector3 pointInLocalSpace = collision.transform.InverseTransformPoint(collisionPoint);
        
        Debug.Log(pointInLocalSpace);
        if (pointInLocalSpace.x < -0.1f)
        {
            motor.DisableMoveLeft(true);
        }else if (pointInLocalSpace.x > 0.1f)
        {
            motor.DisableMoveRight(true);
        }
    }

    protected override void ExitEvent()
    {
        base.ExitEvent();
 
        motor.DisableMoveLeft(false);
        motor.DisableMoveRight(false);
        
    
    }

}
