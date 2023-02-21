using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoSingleton<ScoreManager>
{
   private List<ScoreObject> _scoreObjects;
   public int currentScore;

   public int leftScoreInLevel
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
   protected override void Init()
   {
      base.Init();
      _scoreObjects = new List<ScoreObject>();
      currentScore = 0;
   }

   public void AddScoreObj(ScoreObject so)
   {
      _scoreObjects.Add(so);
   }

   public void ScoreScoreObj(ScoreObject so)
   {
      currentScore += so.score;
      _scoreObjects.Remove(so);
   }
}
