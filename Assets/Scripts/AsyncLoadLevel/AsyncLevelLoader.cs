using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class AsyncLevelLoader : MonoBehaviour
{ 
    [SerializeField] private Animator animator;
    private string _targetSceneName;

    private void Awake()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
   
    private AsyncOperation _asyncOperation;

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.2f);
        if(_targetSceneName == null) Debug.LogError(this.name + " targetSceneName is null");
        _asyncOperation = SceneManager.LoadSceneAsync(_targetSceneName);

        while (!_asyncOperation.isDone)
        {
            //场景加载中
            yield return null;
        }

        
        //场景加载完毕
        FadeOut();
        Invoke(nameof(Destroy), 10f);
    }

    void FadeIn()
    {
        animator.Play("LevelFadeIn");
    }

    void FadeOut()
    {
        animator.Play("LevelFadeOut");
        
    }

    void Destroy()
    {
        Object.Destroy(gameObject);
    }

    public void StartLoadAsync(string sceneName)
    {
        _targetSceneName = sceneName;
        FadeIn();
        StartCoroutine(LoadScene());

    }
}
