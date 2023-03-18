using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorHuman : PlayerMotor
{
    public override void Move()
    {
        if (canMove)
        {
            Vector3 velBeforeSlope = (_nextMoveXVelocity + _nextMoveZVelocity);
        
            Vector3 velAfterSlope = velBeforeSlope;
            RaycastHit hit;
            if (IsOnSlope(out hit))
            {
                var dot = Vector3.Dot(hit.normal, transform.forward);
                //判断是不是在上坡
                if (dot < 0f)
                {
                    velAfterSlope = Vector3.ProjectOnPlane(velBeforeSlope, hit.normal);
                    if (_rigidBody.useGravity)
                    {
                        _rigidBody.useGravity = false;
                    }
                }else 
                {
                    velAfterSlope = Vector3.ProjectOnPlane(velBeforeSlope, hit.normal);
                    if (!_rigidBody.useGravity)
                    {
                        _rigidBody.useGravity = true;
                    }
                }

            }
            else
            {
                velAfterSlope.y = _rigidBody.velocity.y;
                if (!_rigidBody.useGravity)
                {
                    _rigidBody.useGravity = true;
                }
            }
            _rigidBody.velocity = velAfterSlope;
            
        }
    }
}
