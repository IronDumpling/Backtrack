using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXGenerator : MonoBehaviour
{
    [SerializeField] private string VFXPointDirectory  = "VFXPointDirectory";

    

    private GameObject _vfxPrefab;
    private Transform _vfxPointDirectoryTransform;

    private List<GameObject> _vfxPrefabList;
    private void Awake()
    {
        _vfxPrefab = LoadResourceManager.Instance.DustVFXPrefab;
        _vfxPointDirectoryTransform = HierarchyHelper.GetChild(transform, VFXPointDirectory);
        _vfxPrefabList = new List<GameObject>();
        for (int i = 0 ; i < _vfxPointDirectoryTransform.childCount; i++)
        {
            Transform curTr = _vfxPointDirectoryTransform.GetChild(i);
            _vfxPrefabList.Add(Instantiate(_vfxPrefab, curTr));
        }
    }
}
