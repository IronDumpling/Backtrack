using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Common;

public class TriggerURP : TriggerBase
{
    public int assetIdx;

    protected override void enterEvent()
    {
        base.enterEvent();
        URPManager.Instance.SetRendererAsset(assetIdx);
    }
}
