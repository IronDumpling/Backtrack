using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPathBoundary : CollisionBase
{
    private PlayerMotor motor;
    private void Start()
    {
        motor = PlayerController.Instance.GetComponent<PlayerMotor>();
    }

    
    protected override void StayEvent(Collision collision)
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
