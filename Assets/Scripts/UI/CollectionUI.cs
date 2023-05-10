using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    public int _levelNum = 0;
    private GameObject _score;
    private GameObject _rate;

    // Start is called before the first frame update
    void Awake()
    {
        _score = transform.Find("Score")?.gameObject;
        _rate = transform.Find("Rate")?.gameObject;
    }

    private void Start()
    {
        ShowScore();
    }

    // Update is called once per frame
    void ShowScore()
    {
        _score.GetComponent<TMPro.TMP_Text>().text = $"{MonoPlayerData.Instance.Level0Score}";
        _rate.GetComponent<TMPro.TMP_Text>().text = $"{MonoPlayerData.Instance.Level0Score}%";
    }
}
