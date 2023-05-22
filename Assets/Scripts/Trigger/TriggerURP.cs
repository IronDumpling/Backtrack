using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerURP : TriggerBase
{
    public UniversalRenderPipelineAsset urpAsset;
    public string MaterialName;

    // Start is called before the first frame update
    void Awake()
    {
        urpAsset = Resources.Load<UniversalRenderPipelineAsset>("Urp/Universal Render Pipeline Asset_Renderer");
    }

    public void SetBlitMaterial(string materialName)
    {
        Material material = Resources.Load<Material>("Prefabs/VFX/Vfx/Full Screen" + materialName);

        if (material != null)
        {
            //urpAsset.blitMaterial = material;
        }
        else
        {
            Debug.LogError("Failed to find material: " + materialName);
        }
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        SetBlitMaterial(MaterialName);
    }
}
