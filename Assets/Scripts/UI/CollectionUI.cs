using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    public int m_LevelNum = 1;
    private GameObject _score;
    private GameObject _rate;
    private GameObject _collectionRate;

    // Start is called before the first frame update
    void Awake()
    {
        _score = transform.Find("Score")?.gameObject;
        _rate = transform.Find("Rate")?.gameObject;
        _collectionRate = transform.Find("CollectionRate")?.gameObject;
    }

    // Update is called once per frame
    public void ShowScore(int levelNum)
    {
        int currScore = 0;
        int totalScore = 0;
        float rate = 0f;

        List<LevelInfo_SO> dataSoList = new List<LevelInfo_SO>();

        if (levelNum == 1)
        {
            dataSoList.Add(Resources.Load<LevelInfo_SO>("GameData/LevelData/Level0-1"));
            dataSoList.Add(Resources.Load<LevelInfo_SO>("GameData/LevelData/Level0-2"));
            currScore = MonoPlayerData.Instance.Level0Score;
            
        }
        else if (levelNum == 2)
        {
            dataSoList.Add(Resources.Load<LevelInfo_SO>("GameData/LevelData/Level3-1"));
            currScore = MonoPlayerData.Instance.Level3Score;
        }

        foreach (LevelInfo_SO level in dataSoList)
        {
            totalScore += level.levelTotalScore;
        }

        rate = (currScore / (float) totalScore) * 100;

        _collectionRate.GetComponent<TMPro.TMP_Text>().text = "收集率";

        if(totalScore == 0)
            _score.GetComponent<TMPro.TMP_Text>().text = $"{currScore}";
        else
            _score.GetComponent<TMPro.TMP_Text>().text = $"{currScore}/{totalScore}";

        if (rate.ToString("F0") == "NaN")
            _rate.GetComponent<TMPro.TMP_Text>().text = "0%";
        else
            _rate.GetComponent<TMPro.TMP_Text>().text = rate.ToString("F0") + "%";
    }
}
