using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1
{
    public class ObstacleFriend : TriggerBase
    {
       protected override void enterEvent(Collider collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                EventManager.Instance.PlayerDeadEventTrigger();
            }
            
        } 
        
    }
}
