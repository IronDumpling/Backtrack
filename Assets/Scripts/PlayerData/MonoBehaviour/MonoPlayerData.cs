using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class MonoPlayerData : NoDestroyMonoSingleton<MonoPlayerData>
{
    [SerializeField] private PlayerData_SO _dataSo;
    protected override void Init()
    {
        base.Init();
        if (_dataSo == null) _dataSo = Resources.Load<PlayerData_SO>("GameData/PlayerData");
    }

    public int Level0Score
    {
        get => _dataSo.level0Score;
        set => _dataSo.level0Score = value;
    }

    public int Level3Score
    {
        get => _dataSo.level3Score;
        set => _dataSo.level3Score = value;
    }

    public string Level0Choices
    {
        get => _dataSo.level0Choices;
        set => _dataSo.level0Choices = value;
    }

    public int Level4Score
    {
        get => _dataSo.level4Score;
        set => _dataSo.level4Score = value;
    }

    public string Level4Choices
    {
        get => _dataSo.level4Choices;
        set => _dataSo.level4Choices = value;
    }

    public void Reset()
    {
        _dataSo.level0Score = 0;
        _dataSo.level0Choices = "";
        _dataSo.level3Score = 0;
        _dataSo.level4Score = 0;
        _dataSo.level4Choices = "";
    }
}
