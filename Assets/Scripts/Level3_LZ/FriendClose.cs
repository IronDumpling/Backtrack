using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendClose : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Arrow")
        {
            Destroy(gameObject);
        }
    }
}
