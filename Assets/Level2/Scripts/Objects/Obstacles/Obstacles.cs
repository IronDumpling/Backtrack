using UnityEngine;
using System.Collections;
using Backtrack.Core;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
public class Obstacles : MonoBehaviour
{
    const string playerTag = "Player";

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
            Debug.Log("Detect collision");
            GameManager.Instance.Lose();
        }
    }
}

