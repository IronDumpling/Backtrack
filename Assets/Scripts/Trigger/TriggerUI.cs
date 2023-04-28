using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : TriggerBase
{
    [SerializeField] private string _UIName;
    private GameObject _targetUI;

    void Awake()
    {
        _targetUI = GameObject.Find($"/ScreenCanvas/{_UIName}");

        if (_UIName == null) Debug.LogWarning("UI Name is Missing!");
        if(_targetUI == null) Debug.LogWarning("Target UI could not be found!");
    }

    private void Start()
    {
        _targetUI?.SetActive(false);
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        _targetUI?.SetActive(true);
    }

    protected override void ExitEvent()
    {
        base.ExitEvent();
        _targetUI?.SetActive(false);
    }
}
