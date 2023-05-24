using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteAlways]
public class AFX_WeaponTrail : MonoBehaviour
{
    [Header("拖尾组件")]
    private TrailRenderer trailRenderer;
    [Header("偏移强度控制 当拖尾节点数>=此值时达到最大")]
    public int maxCount = 30;
    [Header("最小停止值 拖尾节点数<=此值时偏移强度即归0")]
    public int minCount = 8;
    float aa = 0;
    [Header("材质")]
    public Material mat;
    [Header("当每帧移动小于此值时停止生成拖尾（修正角色待机动作时拖尾原地摆动）")]
    public float stopRange = 0.1f;
    [Header("当前节点数量")]
    public float shuliang = 0f;
    Vector3 lowpos = new Vector3(0,0,0);
    Vector3 newpos = new Vector3(0, 0, 0);
    private void Awake()
    {
        if (!trailRenderer) 
        {
            trailRenderer = gameObject.GetComponent<TrailRenderer>();
        }
    }
    void Start()
    {
        if (!trailRenderer)
        {
            trailRenderer = gameObject.GetComponent<TrailRenderer>();
        }
        if (!mat) 
        {
            mat = trailRenderer.material;
        }
    }
    void Update()
    {
        newpos = transform.position;
       if(Vector3.Distance(newpos, lowpos)>= stopRange)
        {
            trailRenderer.emitting = true;
        }
        else 
        {
            trailRenderer.emitting = false;
        }
        shuliang = trailRenderer.positionCount;
        aa =Mathf.Clamp01((shuliang- minCount) / maxCount) ;
        mat.SetFloat("_VO_Scale", aa);
        lowpos = newpos;
    }
}
