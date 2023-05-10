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
        if (levelNum == 1)
            currScore = MonoPlayerData.Instance.Level0Score;
        else if (levelNum == 2)
            currScore = MonoPlayerData.Instance.Level3Score;

        _collectionRate.GetComponent<TMPro.TMP_Text>().text = "收集率";
        _score.GetComponent<TMPro.TMP_Text>().text = $"{currScore}";
        _rate.GetComponent<TMPro.TMP_Text>().text = $"{currScore}%";
    }
}
