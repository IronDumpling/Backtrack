using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMotorBall : PlayerMotor
{
    public override void Move()
    {
        if (canMove)
        {
            Vector3 velBeforeSlope = (_nextMoveXVelocity + _nextMoveZVelocity);
        
            Vector3 velAfterSlope = velBeforeSlope;

            velAfterSlope.y = _rigidBody.velocity.y;
            Debug.DrawLine(transform.position,transform.position+velAfterSlope,Color.green);

            _rigidBody.velocity = velAfterSlope;
            
        }
    }
}
