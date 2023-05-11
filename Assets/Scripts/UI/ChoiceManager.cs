using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;

public class ChoiceManager : NoDestroyMonoSingleton<ChoiceManager>
{
    public string displayText;

    protected override void Init()
    {
        base.Init();
        displayText = "";
    }

    public void DisplayChoices()
    {
        GameObject.Find("Beat_5_Track/End-Track-L/Instruction_Canvas/Instruction").GetComponent<TMPro.TMP_Text>().text = displayText;
        GameObject.Find("Beat_5_Track/End-Track-R/Instruction_Canvas/Instruction").GetComponent<TMPro.TMP_Text>().text = displayText;
    }
}
