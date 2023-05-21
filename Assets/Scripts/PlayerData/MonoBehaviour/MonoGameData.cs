using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class MonoGameData : NoDestroyMonoSingleton<MonoGameData>
{
    [SerializeField] private GameData_SO _dataSo;
    
    protected override void Init()
    {
        base.Init();
        if (_dataSo == null) _dataSo = Resources.Load<GameData_SO>("GameData/GameData");
    }

    private void Start()
    {
        //_dataSo?.masterResolutionIdx
    }

    public int ResolutionIdx
    {
        get => _dataSo.masterResolutionIdx;
        set => _dataSo.masterResolutionIdx = value;
    }
    
    public float Brightness
    {
        get => _dataSo.masterBrightness;
        set => _dataSo.masterBrightness = value;
    }
    
    public float Volume
    {
        get => _dataSo.masterVolume;
        set => _dataSo.masterVolume = value;
    }
}
