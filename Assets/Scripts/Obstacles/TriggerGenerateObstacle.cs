using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class TriggerGenerateObstacle : TriggerBase
{
    
    [SerializeField] private List<GameObject> GenerateObj;



    protected override void enterEvent(Collider collision)
    {
        base.enterEvent(collision);

        foreach (var obj in GenerateObj)
        {
            Instantiate(obj,  PlayerControllerBase.Instance.transform.Find("ArrowGeneratePlace"));

        }
        
        
    }
}
