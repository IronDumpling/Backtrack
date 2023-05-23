using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Common;

public class URPManager : NoDestroyMonoSingleton<URPManager>
{
    [SerializeField] private List<RenderPipelineAsset> _URPAssetList;

    protected override void Init()
    {
        base.Init();
        _URPAssetList = new List<RenderPipelineAsset>(4);
        _URPAssetList.Add(Resources.Load<RenderPipelineAsset>("Urp/URPAsset/Universal Render Pipeline Asset Default"));
        _URPAssetList.Add(Resources.Load<RenderPipelineAsset>("Urp/URPAsset/Universal Render Pipeline Asset White"));
        _URPAssetList.Add(Resources.Load<RenderPipelineAsset>("Urp/URPAsset/Universal Render Pipeline Asset Pos"));
        _URPAssetList.Add(Resources.Load<RenderPipelineAsset>("Urp/URPAsset/Universal Render Pipeline Asset Neg"));
    }

    public void SetRendererAsset(int assetIdx)
    {
        if (assetIdx < 0 && assetIdx >= _URPAssetList.Count) return;
        
        if (QualitySettings.renderPipeline != null && QualitySettings.renderPipeline != _URPAssetList[assetIdx])
        {
            QualitySettings.renderPipeline = _URPAssetList[assetIdx];
        }
        else if (GraphicsSettings.renderPipelineAsset != _URPAssetList[assetIdx])
        {
            GraphicsSettings.renderPipelineAsset = _URPAssetList[assetIdx];
        }
    }
}