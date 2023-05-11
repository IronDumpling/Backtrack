using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class MonoLevelInfo : MonoSingleton<MonoLevelInfo>
{
    [SerializeField] private LevelInfo_SO _dataSo;
    
    protected override void Init()
    {
        base.Init();
        if (_dataSo == null) Debug.LogError("没有levelinfo");
    }

    private void Start()
    {
        _dataSo.levelTotalScore = ScoreManager.Instance.RemainScoreInLevel;
    }

    public int levelNum
    {
        get => _dataSo.levelNum;
        set => _dataSo.levelNum = value;
    }
    
    public int LevelSceneNum
    {
        get => _dataSo.levelSceneNum;
        set => _dataSo.levelSceneNum = value;
    }
    
    public string LevelBGM
    {
        get => _dataSo.levelBGM;
        set => _dataSo.levelBGM = value;
    }

    public int LevelTotalScore
    {
        get => _dataSo.levelTotalScore;
    }
}
