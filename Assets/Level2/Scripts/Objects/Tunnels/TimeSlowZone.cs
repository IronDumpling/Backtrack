using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowZone : MonoBehaviour
{
    const string playerTag = "Player";
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
            Time.timeScale = 0.5f;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
            Time.timeScale = 1.0f;
        }
    }
}
