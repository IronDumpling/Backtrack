using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : NoDestroyMonoSingleton<ScoreManager>
{
   private List<ScoreObject> _scoreObjects;
   [SerializeField] private int currentScore;

   [SerializeField] private int level0TotalScores;
   public event Action onAfterScoreAnObj;
   public int RemainScoreInLevel
   {
      get
      {
         int total = 0;
         for (var i = 0; i < _scoreObjects.Count; i++)
         {
            total += _scoreObjects[i].score;
         }

         return total;
      }
   }

   public int CurrentScoreInLevel
   {
      get => currentScore;
      set
      {
         currentScore = value;
         onAfterScoreAnObj?.Invoke();
      }
   }


   protected override void Init()
   {
      base.Init();
      _scoreObjects = new List<ScoreObject>();
      currentScore = 0;
      
      SceneManager.sceneLoaded += OnSceneLoaded;
   }

   public void AddScoreObj(ScoreObject so)
   {
      _scoreObjects.Add(so);
   }
   public void RemoveScoreObj(ScoreObject so)
   {
      _scoreObjects.Remove(so);
   }

   public void ScoreScoreObj(ScoreObject so)
   {
      //this.transform.position = so.transform.position;
      currentScore += so.score;
      _scoreObjects.Remove(so);
      onAfterScoreAnObj?.Invoke();
   }

   public void ResetScore()
   {
      //TODO: reset scores
   }

   public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
      _scoreObjects = new List<ScoreObject>();
   }
   

}
