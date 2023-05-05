using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : NoDestroyMonoSingleton<AudioManager>
{
    public List<AudioType> audioList;

    protected override void Init()
    {
        foreach (AudioType type in audioList)
        {
            type.source = gameObject.AddComponent<AudioSource>();
            type.source.clip = type.clip;
            type.source.name = type.name;
            type.source.volume = type.volume;
            type.source.pitch = type.pitch;
            type.source.loop = type.isLoop;

            if (type.group != null)
            {
                type.source.outputAudioMixerGroup = type.group;
            }
        
        }
    }

    public void Play(string audioName)
    {
        foreach (var type in audioList)
        {
            if (type.name == audioName)
            {
                type.source.Play();
                return;
            }
        }
        
        Debug.LogWarning("没有找到" + audioName + " 音频");
    }
    
    public void Pause(string audioName)
    {
        foreach (var type in audioList)
        {
            if (type.name == audioName)
            {
                type.source.Pause();
                return;
            }
        }
        Debug.LogWarning("没有找到" + name + " 音频");
    }
    
    public void Stop(string audioName)
    {
        foreach (var type in audioList)
        {
            if (type.name == audioName)
            {
                type.source.Stop();
                return;
            }
        }
        Debug.LogWarning("没有找到" + name + " 音频");
    }

    public void PlayAll()
    {
        foreach (var type in audioList)
        {
            type.source.Play();
        }
    }

    public void PauseAll()
    {
        foreach (var type in audioList)
        {
            type.source.Pause();
        }
    }
    
    public void StopAll()
    {
        foreach (var type in audioList)
        {
           
            type.source.Stop();
            type.source.time = 0;
         

        }

    }

    public string GetMusicIsPlaying()
    {
        foreach (var type in audioList)
        {
            if (type.source.isPlaying)
            {
                return type.name;
            }
        }

        return null;
    }
    public float GetMusicTime(string aName)
    {
        foreach (var type in audioList)
        {
            if (type.name == aName)
            {
                return type.source.time;
            }
        }

        return 0f;
    }

    public void SetMusicTime(string mname, float mtime)
    {
        foreach (var type in audioList)
        {
            if (type.name == mname)
            {
                type.source.time = mtime;
            }
        }
    }

    public void PlayMusicAtStart()
    {
        if (SavePointManager.Instance.isSave)
        {
           
        }
        else
        {
            Play(PlayerController.Instance.levelBGM);
        }
    }

}
[System.Serializable]
public class AudioType
{
    [HideInInspector]
    public AudioSource source;
    public AudioClip clip;
    public AudioMixerGroup @group;

    public string name;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,5f)]
    public float pitch;
    public bool isLoop;
}

