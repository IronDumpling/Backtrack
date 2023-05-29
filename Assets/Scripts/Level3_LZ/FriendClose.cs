using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendClose : MonoBehaviour
{
    private BoxCollider boxCollider;
    private void Start() {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Arrow")
        {
            boxCollider.enabled = false;
            Destroy(gameObject);
            Time.timeScale = 1f;
        }
    }
}
