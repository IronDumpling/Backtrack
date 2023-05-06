using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : TriggerBase
{
   public int score = 1;
   public AudioClip scoreSound;
   public GameObject scoreEffect;
   private AudioSource audioSource;

   private void Start()
   {
      audioSource = GetComponent<AudioSource>();
      ScoreManager.Instance.AddScoreObj(this);
   }

   protected override void enterEvent()
   {
      base.enterEvent();
      ScoreManager.Instance.ScoreScoreObj(this);
      audioSource.PlayOneShot(scoreSound);
      //模型所有贴图消失
      Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
      foreach (Renderer renderer in renderers)
      {
         renderer.enabled = false;
      }

      //this.gameObject.SetActive(false);
      Instantiate(scoreEffect,  transform.position, Quaternion.identity, transform);
      Invoke("Disable", 0.3f);//延迟0.1秒后执行Disable方法
   }
   void Disable()
   {
      
      this.gameObject.SetActive(false);
      
   }
   
}
