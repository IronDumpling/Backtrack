using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TriggerSceneChange : TriggerBase
{
    [SerializeField] private int _sceneIndex = 1;

    protected override void enterEvent()
    {
        Debug.Log("Change Scene");
        base.enterEvent();
        SceneManager.LoadScene(_sceneIndex);
    }

}
