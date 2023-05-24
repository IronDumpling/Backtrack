using System.Collections;
using System.Collections.Generic;
using AtmosphericHeightFog;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class TriggerFog : TriggerBase
{
    public float dur = 10f;
    protected override void enterEvent()
    {
        base.enterEvent();
        HeightFogGlobal fog = transform.GetChild(0).GetComponent<HeightFogGlobal>();
        DOTween.To(() =>  fog.fogIntensity , x =>  fog.fogIntensity = x ,1,dur);
    }
}
