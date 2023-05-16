using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIconManager : MonoBehaviour
{ 
    [SerializeField] private string _unlockLevel = "Animation/Animator/UnlockLevelIconController";
    [SerializeField] private string _lockedLevel = "Animation/Animator/LockedLevelIconController";
    private PlayerData_SO _playerData;
    [SerializeField] private List<Animator> _levelList;

    private void Awake()
    {
        for (int idx = 0; idx < transform.childCount; idx++)
        {
            _levelList.Add(transform?.GetChild(idx)?.gameObject?.GetComponent<Animator>());
        }
        _playerData = Resources.Load<PlayerData_SO>("GameData/PlayerData");
    }

    // Start is called before the first frame update
    void Start()
    {
        UnLockedIconSettings(0);

        if (_playerData.level0Score == 0) LockedIconSettings(1); 
        else UnLockedIconSettings(1);

        if (_playerData.level3Score == 0) LockedIconSettings(2);
        else UnLockedIconSettings(2);
    }

    void LockedIconSettings(int idx)
    {
        if (_levelList[idx] == null)
        {
            Debug.LogWarning($"Level {idx + 1} Icon Animator is Missing");
            return;
        }
        _levelList[idx].gameObject.GetComponent<Button>().enabled = false;
        _levelList[idx].runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_lockedLevel);
    }

    void UnLockedIconSettings(int idx)
    {
        if (_levelList[idx] == null)
        {
            Debug.LogWarning($"Level {idx + 1} Icon Animator is Missing");
            return;
        }
        _levelList[idx].gameObject.GetComponent<Button>().enabled = true;
        _levelList[idx].runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_unlockLevel);
    }
}