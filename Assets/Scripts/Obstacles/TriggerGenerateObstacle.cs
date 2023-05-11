using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class TriggerGenerateObstacle : TriggerBase
{
    
    [SerializeField] private GameObject GenerateObj;



    protected override void enterEvent(Collider collision)
    {
        base.enterEvent(collision);
        Instantiate(GenerateObj,  PlayerControllerBase.Instance.transform.Find("ArrowGeneratePlace"));
        
        
    }
}
