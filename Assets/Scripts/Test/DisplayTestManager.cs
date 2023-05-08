
using Common;
using Unity.Collections;
using UnityEngine;

public class DisplayTestManager : MonoSingleton<DisplayTestManager>
{
    [Header("Score Manger Display")]
    [SerializeField, ReadOnly] private int remainScore;
    [SerializeField, ReadOnly] private int currentScore;
    [SerializeField, ReadOnly] private int totalScore;

    
    
    //void Update()
    //{
    //     remainScore = ScoreManager.Instance.RemainScoreInLevel;
    //     currentScore = ScoreManager.Instance.CurrentScoreInLevel;
    //     totalScore = ScoreManager.Instance.TotalScoreInLevel;
    // }
}
