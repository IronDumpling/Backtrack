using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
 
public class Video_Manager : MonoBehaviour
{
    double video_time, currentTime;
    //这里的video_img我是用来放RawImage的，挂载脚本后将RawImage拖入即可
    public GameObject video_img;
    void Start()
    {
        video_time = video_img.GetComponent<VideoPlayer>().clip.length;
    }
 
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= video_time)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        
        SceneManager.LoadScene("Level0_DESIGN");
    }
}