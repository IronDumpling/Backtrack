using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LoadResourceManager : MonoSingleton<LoadResourceManager>
{
    [Header("Resource.Load的参数")] 
    [SerializeField] private string DustVFXAssetFilePath = "Prefabs/VFX/Dust";

    private GameObject _dustVFXPrefab;
    public GameObject DustVFXPrefab
    {
        get
        {
            if (_dustVFXPrefab == null)
            {
                _dustVFXPrefab = (GameObject)Resources.Load(DustVFXAssetFilePath);
                if (_dustVFXPrefab == null)
                {
                    Debug.LogError("未在Resources文件中找到" + nameof(DustVFXPrefab) + "相关的Prefab");
                }
            }
            return _dustVFXPrefab;
        }
    }

}
