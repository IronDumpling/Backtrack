using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.onAfterScoreAnObj += UpdateScore;
    }

    void UpdateScore()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().text = $"Score {ScoreManager.Instance.CurrentScoreInLevel}";
    }

    private void OnDisable()
    {
        if(ScoreManager.Instance != null) ScoreManager.Instance.onAfterScoreAnObj -= UpdateScore;
    }
}
