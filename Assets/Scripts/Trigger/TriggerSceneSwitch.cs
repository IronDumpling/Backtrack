using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TriggerSceneSwitch : TriggerBase
{
    [SerializeField] private string _switchSceneName;
    [SerializeField] private string[] _sceneNames;

#if UNITY_EDITOR
    private void Awake()
    {
        int count = EditorBuildSettings.scenes.Length;
        _sceneNames = new string[count];

        for (int i = 0; i < count; i++)
        {
            _sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
        }
    }
#endif

    protected override void enterEvent()
    {
        
        base.enterEvent();

        if(_switchSceneName == null)
        {
#if UNITY_EDITOR
            SceneManager.LoadScene(_sceneNames[0]);
#endif
            Debug.Log("The Switch Scene's Name is Missing");
        }  
        else
            SceneManager.LoadScene(_switchSceneName);
    }

}
