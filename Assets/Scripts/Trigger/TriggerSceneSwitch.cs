using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TriggerSceneSwitch : TriggerBase
{
    [SerializeField] private string _switchSceneName;
    
    protected override void enterEvent()
    {
        EventManager.Instance.PlayerEnterNewSceneEventTrigger(_switchSceneName);
        
    }

}
