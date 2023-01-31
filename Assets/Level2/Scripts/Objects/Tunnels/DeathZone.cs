using UnityEngine;
using System.Collections;
using Backtrack.Core;

public class DeathZone : MonoBehaviour
{
    const string playerTag = "Player";

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
            Debug.Log("Detect collision");
            GameManager.Instance.Lose();
        }
    }
}

