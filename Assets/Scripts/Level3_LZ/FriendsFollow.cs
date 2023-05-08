using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsFollow : MonoBehaviour
{
    public Transform player;  // 玩家的 Transform 组件
    public Camera mainCamera;
    private bool isFollow = false;  // 是否跟随玩家
    private Vector3 screenPos; 
    private void OnTriggerEnter(Collider other) {
       if (other.gameObject.tag == "Player")
        {
            screenPos = mainCamera.WorldToScreenPoint(transform.position);
            isFollow = true;
        }
    }
    private void Update()
    {
        // 将敌人的世界坐标转换为屏幕坐标
        if(isFollow)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, screenPos.z));
            

            // 更新敌人的位置
            transform.position = worldPos;
        }
       
    }
}
