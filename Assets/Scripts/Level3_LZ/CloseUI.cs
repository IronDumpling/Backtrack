using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUI : MonoBehaviour
{
    public GameObject UI;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            UI.SetActive(false);
        }
    }
}
