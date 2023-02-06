using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointTriggerBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        transform.parent.GetComponent<CurveRotationPath>().StartPointBlockTriggered(collision, transform);
    }
}
