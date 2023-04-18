using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;
using Color = UnityEngine.Color;

public class PlayerMotorBall : PlayerMotor
{
    private SphereCollider _sphereCollider;

    [SerializeField] private float rayOffset = 0.2f;
    protected override void Init()
    {
        base.Init();
        _sphereCollider = GetComponent<SphereCollider>();
    }

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

    public override void CheckPathBoundary()
    {
        DisableMoveLeft(false);
        DisableMoveRight(false);
        
        float colliderRadius = _sphereCollider.radius;
        //如果往左发射射线碰到东西
        bool isRayLeft1 = false;
        bool isRayLeft2 = false;
        bool isRayRight1 = false;
        bool isRayRight2 = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right,
                out hit, colliderRadius, groundLayer))
        {
            isRayLeft1 = true;
        }
        //如果往右发射射线碰到东西
        if (Physics.Raycast(transform.position, transform.right,
                out hit, colliderRadius, groundLayer))
        {
            isRayRight1 = true;
        }
        Vector3 rayLeft1EndPos = transform.position + (-transform.right * colliderRadius);
        //如果往下碰到东西
        if (Physics.Raycast(rayLeft1EndPos, -transform.up,
                out hit, colliderRadius + rayOffset, groundLayer))
        {
            isRayLeft2 = true;
        }

        Vector3 rayRight1EndPos = transform.position + (transform.right * colliderRadius);
        if (Physics.Raycast(rayRight1EndPos , -transform.up,
                out hit, colliderRadius + rayOffset, groundLayer))
        {
            isRayRight2 = true;
        }
        //往下射线，如果往左射线没碰到东西，往下也没碰到，才禁左
        if (isRayLeft1)
        {
            DisableMoveLeft(true);
        }

        if (isRayRight1)
        {
            DisableMoveRight(true);
        }

        if (!isRayLeft1 && !isRayLeft2 && isRayRight2)
        {
            DisableMoveLeft(true);
        }
        if (!isRayRight1 && !isRayRight2 && isRayLeft2)
        {
            DisableMoveRight(true);
        }
    }


}
