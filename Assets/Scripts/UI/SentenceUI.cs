using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceUI : MonoBehaviour
{
    private Dictionary<string, string> sentenceDict = new Dictionary<string, string>
    {
        { "你苏醒到达未来", "不接受赠予的人也将被遗忘。" },
        { "你苏醒到达过去", "伊始之地，下坠沉溺。" },
        { "你苏醒离开未来", "伊始之地，下坠沉溺。" },
        { "你苏醒离开过去", "不接受赠予的人也将被遗忘。" },
        { "你挣扎到达未来", "恐惧蔓延至全身。" },
        { "你挣扎到达过去", "伊始之地，不堪回首。" },
        { "你挣扎离开未来", "伊始之地，不堪回首。" },
        { "你挣扎离开过去", "你出生了，在你想要出生之前。" },
        { "我苏醒到达未来", "你出生了，在你准备出生之前。" },
        { "我苏醒到达过去", "是故地，是轮回，是回溯。" },
        { "我苏醒离开未来", "成为它，我变成了它。" },
        { "我苏醒离开过去", "你们最想要的结局。" },
        { "我挣扎到达未来", "你们最想要的结局。" },
        { "我挣扎到达过去", "迷失混沌，障目一叶。" },
        { "我挣扎离开未来", "成为它，我变成了它。" },
        { "我挣扎离开过去", "占据这里成为新的主导。" },
        { "", "我心里危险的东西。"}
    };

    private GameObject _sentence;
    private GameObject _collectUI;
    private float _collectRate;
    private PlayerData_SO _playerData;
    private string _fullSentence = "";

    [SerializeField] private int _levelNum = 0;
    [Tooltip("句子显示分为多少个等级")]
    [SerializeField] private int _displayRank = 4;
    [Tooltip("每个句子显示等级对应的最低收集率(100分制)")]
    [SerializeField] private List<float> _rateRangeList;
    [Tooltip("每个句子显示等级对应的句子显示比率(100分制)")]
    [SerializeField] private List<float> _rankPercList;

    private List<Tuple<float, float>> _rateMapRankList;

    // Start is called before the first frame update
    void Awake()
    {
        _sentence = transform.Find("CollectedSentence")?.gameObject;
        _collectUI = GameObject.Find("CollectionUI");
        _playerData = Resources.Load<PlayerData_SO>("GameData/PlayerData");
        _fullSentence = sentenceDict[_playerData.level0Choices];
        if (_rateRangeList == null) _rateRangeList = new List<float>(_displayRank);
        if (_rankPercList == null) _rankPercList = new List<float>(_displayRank);
    }

    private void Start()
    {
        _collectUI.GetComponent<CollectionUI>().ShowScore(_levelNum);
        _collectRate = _collectUI.GetComponent<CollectionUI>().CollectRate;
        FillRateMapRankList();
        HidePartialSentence();
        ShowSentence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillRateMapRankList()
    {
        _rateMapRankList = new List<Tuple<float, float>>(_displayRank);

        if (_rateRangeList.Count != _displayRank ||
            _rankPercList.Count != _displayRank)
        {
            Debug.Log($"Rate Range List: {_rateRangeList.Count}, Rank Percent List: {_rankPercList.Count}, Rate Map Rank List: {_displayRank}");
            Debug.LogError("The length of two lists don't match rank number.");
            return;
        }

        for (int idx = 0; idx < _displayRank; idx++)
        {
            _rateMapRankList.Add(new Tuple<float, float>(_rateRangeList[idx] / 100f,
                                                         _rankPercList[idx] / 100f));
        }
    }

    void HidePartialSentence()
    {
        float rankPerc = _rateMapRankList[_rateMapRankList.Count - 1].Item2;

        for (int i = 1; i < _rateMapRankList.Count; i++)
        {
            if (_collectRate < _rateMapRankList[i].Item1)
            {
                rankPerc = _rateMapRankList[i - 1].Item2;
                Debug.Log($"collection rate: {_collectRate}, rank: {i - 1}, rank perc: {rankPerc}");
                break;
            }
        }

        int length = _fullSentence.Length;
        int showLength = (int)(length * rankPerc);

        Debug.Log($"Full Length: {length}, Show Length: {showLength}");

        _sentence.GetComponent<TMPro.TMP_Text>().text = $"{_fullSentence.Substring(0, showLength)}";
    }

    void ShowSentence()
    {

    }
}
