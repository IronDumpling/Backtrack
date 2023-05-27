using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectTriggle : MonoBehaviour
{
    [Range(0, 1)]
    public float volume = 0.5f;
    
    private void OnTriggerEnter(Collider other) {
        if (gameObject.tag!="AudioChange"&&other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<AudioSource>().enabled = true;
        }
        if(gameObject.tag=="AudioChange"&&other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponent<AudioSource>().volume = volume;
        }
    }
}
