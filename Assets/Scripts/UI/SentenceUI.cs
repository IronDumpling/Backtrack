using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceUI : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> sentenceDict = new Dictionary<string, string>
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
    private PlayerData_SO _playerData;
    private string _fullSentence = "";

    // Start is called before the first frame update
    void Awake()
    {
        _sentence = transform.Find("CollectedSentence")?.gameObject;
        _playerData = Resources.Load<PlayerData_SO>("GameData/PlayerData");
        _fullSentence = sentenceDict[_playerData.level0Choices];
    }

    private void Start()
    {
        HidePartialSentence();
        ShowSentence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HidePartialSentence()
    {

    }

    void ShowSentence()
    {
        _sentence.GetComponent<TMPro.TMP_Text>().text = $"{_fullSentence}";
    }
}
