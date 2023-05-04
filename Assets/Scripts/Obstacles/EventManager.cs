using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : Singleton<EventManager>
{
   public void PlayerDeadEventTrigger()
   {
      //玩家死亡
      Debug.Log("玩家死亡");
      //播放动画
      // 暂时未实现
      
      //重置关卡   可能会变更的点： （展示从新开始UI -》 从新开始）
      AudioManager.Instance.StopAll();
      DOTween.Clear();
      PlayerController.Instance.GameEnd();
      
      // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      SavePointManager.Instance.LoadSavePoint();
      
   }

   public void PlayerEnterNewSceneEventTrigger(string switchSceneName)
   {
      Debug.Log("进入新场景");
      GameObject asyncLoadObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MapObject/AsyncLevelObject"));

      DOTween.Clear();
      asyncLoadObject.SetActive(true);
      
      asyncLoadObject.GetComponent<AsyncLevelLoader>().StartLoadAsync(switchSceneName);

   }

   public void PlayerVictoryEventTrigger()
   {
      //savepoint manager clean all
   }
   //触发音效
   //给音效一个混响效果
   //让玩家判断左右
}
