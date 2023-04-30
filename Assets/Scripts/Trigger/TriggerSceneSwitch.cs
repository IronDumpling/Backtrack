using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TriggerSceneSwitch : TriggerBase
{
    [SerializeField] private string _switchSceneName;
    [SerializeField] private string[] _sceneNames;
    [SerializeField] private bool isUsingAsyncLoad = true;
    [SerializeField] private string asyncObjectPath = "Prefabs/MapObject/AsyncLevelObject";
    private GameObject asyncLoadObject;


    private void Awake()
    {
#if UNITY_EDITOR
        int count = EditorBuildSettings.scenes.Length;
        _sceneNames = new string[count];

        for (int i = 0; i < count; i++)
        {
            _sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
        }
#endif
        asyncLoadObject = Instantiate(Resources.Load<GameObject>(asyncObjectPath));
    }

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
        {
            if (asyncLoadObject == null || !isUsingAsyncLoad)
            {
                SceneManager.LoadScene(_switchSceneName);
            }
            else
            {
                asyncLoadObject.SetActive(true);
                asyncLoadObject.GetComponent<AsyncLevelLoader>().StartLoadAsync(_switchSceneName);
            }
            
        }
        
        
        
        
    }

}
