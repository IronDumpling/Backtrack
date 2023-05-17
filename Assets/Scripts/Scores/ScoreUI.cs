using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScoreUI : MonoBehaviour
{
    private Animator _ani;

    
    void Awake()
    {
        ScoreManager.Instance.onAfterScoreAnObj += UpdateScore;
        _ani = gameObject.GetComponent<Animator>();
    }

    void UpdateScore()
    {
        _ani.SetTrigger("ScoreChange");
        gameObject.GetComponent<TMPro.TMP_Text>().text = $"{ScoreManager.Instance.CurrentScoreInLevel}";
    }

    private void OnDisable()
    {
        if(ScoreManager.Instance != null) ScoreManager.Instance.onAfterScoreAnObj -= UpdateScore;
    }


}
