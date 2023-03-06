using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalPlayer : MonoBehaviour
{
    private PlayableDirector timeLine;
    // Start is called before the first frame update
    void Start()
    {
        timeLine=GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player")
        {
            timeLine.Play();
        }
    }
}
