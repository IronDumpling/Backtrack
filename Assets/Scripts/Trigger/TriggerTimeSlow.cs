using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTimeSlow : TriggerBase
{
    protected override void StayEvent()
    {
        base.StayEvent();
        Time.timeScale = 0.3f;
    }

    protected override void ExitEvent()
    {
        base.ExitEvent();
        Time.timeScale = 1f;
    }

    public void SetTimeBack()
    {
        Time.timeScale = 1f;
    }
    
}
