using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : TriggerBase
{
   public int score = 1;

   private void Start()
   {
      ScoreManager.Instance.AddScoreObj(this);
   }

   protected override void enterEvent()
   {
      base.enterEvent();
      ScoreManager.Instance.ScoreScoreObj(this);
      this.gameObject.SetActive(false);
   }
}
