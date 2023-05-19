using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWindField : TriggerBase
{
    private Rigidbody rb;
    public Transform startPoint;
    public Transform endPoint;
    public AnimationCurve curve;
    private float maxX;

    private void Start()
    {
        rb = PlayerController_L3.Instance.transform.Find("BirdSlot").GetComponent<Rigidbody>();
        maxX = startPoint.transform.InverseTransformPoint(endPoint.position).x; 

    }

    protected override void StayEvent()
    {
        base.StayEvent();
        float windX = startPoint.transform.InverseTransformPoint(rb.transform.position).x;
        float ratio = windX / maxX;
        rb.AddForce(rb.transform.right * 10000f * curve.Evaluate(ratio), ForceMode.Force);
    }
}
