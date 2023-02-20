using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;//音频播放器
    private Dictionary<string, AudioClip> dictAudio;//字典，储存已加载的音频文件
    public float Volum;//音量

    private void Awake()
    {
        instance = this;//单例模式
        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);
    }
    private AudioClip GetAudio(string path)
    {
        if (!dictAudio.ContainsKey(path))
        {
            dictAudio[path] = LoadAudio(path);
        }
        return dictAudio[path];
    }

    public void PlayBgm(string name,float volum=1.0f)
    {
        audioSource.Stop();
        audioSource.clip = GetAudio(name);
        audioSource.volume = volum;
        audioSource.Play();
    }
    public void StopBgm()
    {
        audioSource.Stop();
    }

    public void PlaySound(string name, float volum = 1.0f)
    {
        this.audioSource.PlayOneShot(LoadAudio(name), volum);
    }
    public void PlaySound(AudioSource audioSource, string name, float volum = 1.0f)
    {
        audioSource.PlayOneShot(LoadAudio(name), volum);
    }

}
