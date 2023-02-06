using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1
{


    public class Obstacles : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                EventManager.Instance.PlayerDeadEventTrigger();
            }
        }
    
    }
}