using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;

public class ChoiceManager : NoDestroyMonoSingleton<ChoiceManager>
{
    public string displayText;
    public List<string> choices;

    protected override void Init()
    {
        base.Init();
        
        choices = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            choices.Add("");
        }

        DisplayChoices();
    }

    public void DisplayChoices()
    {
        generateString();
        GameObject.Find("Beat_5_Track/End-Track-L/Instruction_Canvas/Instruction").GetComponent<TMPro.TMP_Text>().text = displayText;
        GameObject.Find("Beat_5_Track/End-Track-R/Instruction_Canvas/Instruction").GetComponent<TMPro.TMP_Text>().text = displayText;
    }

    public void generateString()
    {
        displayText = "";
        foreach (string choice in choices)
        {
            displayText += choice;
        }
        Debug.Log(displayText);
    }
}
