using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cyan;

public class TriggerURP : TriggerBase
{
    public Blit URPBlit;
    public string m_MaterialName;

    // Start is called before the first frame update
    void Awake()
    {
        if (URPBlit == null) Debug.LogError("URP Blit is Missing!");
    }

    public void SetBlitMaterial(string materialName)
    {
        if(materialName == "")
        {
            URPBlit.settings.blitMaterial = null;
            return;
        }

        Material material = Resources.Load<Material>("Prefabs/VFX/Vfx/Full Screen/" + materialName);

        if (URPBlit != null && material != null) URPBlit.settings.blitMaterial = material;
        else Debug.LogError("Failed to find material: " + materialName);
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        SetBlitMaterial(m_MaterialName);
    }
}
