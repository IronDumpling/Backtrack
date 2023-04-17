using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : MonoBehaviour
{
    private float PauseTime;
    private void Awake() {
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseTime+=Time.deltaTime;
        if(PauseTime<3f){
            Time.timeScale=0.1f;
        }
        else{
            Time.timeScale=1f;
        }
    }
}
