using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEffect : MonoBehaviour
{
    public float TimePausePlayMusic = 0.5f;
    public AudioClip ArrowEffectClip;
    [SerializeField]
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayArrowEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator  PlayArrowEffect()
    {
        yield return new WaitForSeconds(TimePausePlayMusic);
        Debug.Log("PlayArrowEffect");
        _audioSource.PlayOneShot(ArrowEffectClip);
        
    }
}
