using UnityEngine;
using System.Collections;
using Backtrack.Core;

public class FinishZone : MonoBehaviour
{
    const string playerTag = "Player";

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
            Debug.Log("Detect collision");
            GameManager.Instance.Win();
        }
    }
}

