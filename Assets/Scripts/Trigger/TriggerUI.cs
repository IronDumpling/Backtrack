using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : TriggerBase
{
    [SerializeField] private string _UIName;
    [SerializeField] private bool _isText = false;
    private GameObject _targetUI;
    private Animator _ani;

    void Awake()
    {
        _targetUI = GameObject.Find($"/ScreenCanvas/{_UIName}");

        if (_UIName == null) Debug.LogWarning("UI Name is Missing!");
        if(_targetUI == null) Debug.LogWarning("Target UI could not be found!");
    }

    private void Start()
    {
        if (!_isText) _targetUI?.SetActive(false);
        else _ani = _targetUI.GetComponent<Animator>();
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        if (!_isText) _targetUI?.SetActive(true);
        else _ani.SetTrigger("Enable");
    }

    protected override void ExitEvent()
    {
        base.ExitEvent();
        if (!_isText) _targetUI?.SetActive(false);
        else _ani.SetTrigger("Disable");
    }
}
