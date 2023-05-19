using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingMenuManager : MonoBehaviour
{
    #region 调分辨率
    public Resolution[] Resolutions;
    private AudioMixer _audioMixer;

    private GameObject _resolution;
    private GameObject _volume;
    private GameObject _brightness;

    private void Awake()
    {
        Resolutions = Screen.resolutions;
        _resolution = transform.Find("Resolution").gameObject;
        _volume = transform.Find("Volume").gameObject;
        _brightness = transform.Find("Brightness").gameObject;
        _audioMixer = Resources.Load<AudioMixer>("Aduio/Mixer/GameMixer");
    }

    public string[] ResolutionStrings()
    {
        string[] sArr = new string[Resolutions.Length];
        for (var i = 0; i < Resolutions.Length; i++)
        {
            sArr[i] =  i + ": " + Resolutions[i].ToString();
        }

        return sArr;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            Screen.fullScreen = true;
            Debug.Log("全屏");
        }
        else
        {
            Screen.fullScreen = false;
            Debug.Log("窗口化");
        }
    }

    public void SetResolution(int listIndex)
    {
        Resolution resolution = Resolutions[listIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen,resolution.refreshRate);
        Debug.Log("当前分辨率设置为 " + resolution);
    }


    #endregion

    #region 调音量

    public void ChangeVolume()
    {
        
    }

    public void SetVolume (float volume)
    {
        _audioMixer.SetFloat("MasterVolume", volume);
    }

    #endregion

    #region 调亮度


    #endregion


    private int index = 0;



    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 50), "change resolution++"))
    //    {
    //        index++;
    //        SetResolution(index);
    //    }
        
    //    if (GUI.Button(new Rect(200, 10, 100, 50), "change resolution--"))
    //    {
    //        index--;
    //        SetResolution(index);
    //    }
        
    //    if (GUI.Button(new Rect(400, 10, 100, 50), "change fullscreen"))
    //    {
    //        if (Screen.fullScreen)
    //        {
    //            SetFullScreen(false);
    //        }
    //        else
    //        {
    //            SetFullScreen(true);
    //        }
    //    }
    //    if (GUI.Button(new Rect(600, 10, 100, 50), "show all resolution"))
    //    {
    //        string[] s = ResolutionStrings();
    //        foreach (var s1 in s)
    //        {
    //            Debug.Log(s1);
    //        }
    //    }
    //}

    
}

