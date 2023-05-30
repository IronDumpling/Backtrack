using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;

public class TriggerChoiceRecord : TriggerBase
{
    public int index = 1;
    public string choice = "";

    protected override void enterEvent()
    {
        base.enterEvent();
        ChoiceManager.Instance.choices[index-1] = choice;
        if(ChoiceManager.Instance.isDisplay) ChoiceManager.Instance.DisplayChoices();
    }
}