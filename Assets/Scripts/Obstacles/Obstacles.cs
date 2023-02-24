using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Level1
{


    public class Obstacles : TriggerBase
    {


        protected override void enterEvent()
        {
            EventManager.Instance.PlayerDeadEventTrigger();

        }
    }
}