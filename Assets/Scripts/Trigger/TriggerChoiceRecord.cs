using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;

public class TriggerChoiceRecord : TriggerBase
{
    public string word;

    protected override void enterEvent()
    {
        base.enterEvent();
        ChoiceManager.Instance.displayText += word;
        ChoiceManager.Instance.DisplayChoices();
        Debug.Log($"{ChoiceManager.Instance.displayText}");
    }
}