#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine.UI;
#endif
using UnityEngine;

[ExecuteAlways]
public class KWS_CheckDemoSettings : MonoBehaviour
{
#if UNITY_EDITOR
    public Text Cinemachine;
    public Text Postprocessing;
    public Text Linear;

    ListRequest packagesList;

    bool isInitialized;

    void OnEnable()
    {
        packagesList = Client.List();
        isInitialized = false;
    }

    
    void Update()
    {
        if(!isInitialized && packagesList.IsCompleted)
        {
            isInitialized = true;
            var isCinemachineInstalled = false;
            var isPostprocessingInstalled = false;

            var packages = packagesList.Result;
            if (packages != null)
            {
                foreach (var package in packages)
                {
                    if (package.name.Contains("cinemachine")) isCinemachineInstalled = true;
                    else if (package.name.Contains("postprocessing")) isPostprocessingInstalled = true;
                }
            }
            if(Cinemachine != null) Cinemachine.enabled = !isCinemachineInstalled;
            if(Postprocessing != null) Postprocessing.enabled = !isPostprocessingInstalled;
            if (Linear != null) Linear.enabled = PlayerSettings.colorSpace != ColorSpace.Linear;
        }
    }
#endif
}
