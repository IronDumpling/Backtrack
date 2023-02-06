using System.Collections;
using System.Collections.Generic;
using Common;
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
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);

   }
   //触发音效
   //给音效一个混响效果
   //让玩家判断左右
}
