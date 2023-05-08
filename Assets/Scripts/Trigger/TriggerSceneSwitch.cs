using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TriggerSceneSwitch : TriggerBase
{
    [SerializeField] private string _switchSceneName;
    [SerializeField] private bool isVictoryTrigger = false;
    protected override void enterEvent()
    {
        if (isVictoryTrigger)
        {
            EventManager.Instance.PlayerVictoryEventTrigger();
        } 
        EventManager.Instance.PlayerEnterNewSceneEventTrigger(_switchSceneName);
        
    }

}
