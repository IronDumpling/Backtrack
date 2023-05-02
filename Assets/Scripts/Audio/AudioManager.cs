using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    public List<AudioType> audioList;

    protected override void Init()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
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
        Debug.LogWarning("没有找到" + name + " 音频");
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
    
    public void StopAll()
    {
        foreach (var type in audioList)
        {
            type.source.Stop();
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

