using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;

public class ChoiceManager : NoDestroyMonoSingleton<ChoiceManager>
{
    public string displayText;
    public List<string> choices;
    private PlayerData_SO _playerData;

    protected override void Init()
    {
        base.Init();
        
        _playerData = Resources.Load<PlayerData_SO>("GameData/PlayerData");

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
        _playerData.level0Choices = displayText;
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
