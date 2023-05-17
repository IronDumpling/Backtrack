using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWindField : TriggerBase
{
    private Rigidbody rb;

    private void Start()
    {
        rb = PlayerController_L3.Instance.transform.Find("BirdSlot").GetComponent<Rigidbody>();
    }

    protected override void StayEvent()
    {
        base.StayEvent();
        rb.AddForce(rb.transform.right * 1000f);
    }
}
