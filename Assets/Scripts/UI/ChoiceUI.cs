using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;

public class ChoiceManager : NoDestroyMonoSingleton<ChoiceManager>
{
    public string displayText;
    [SerializeField] List<GameObject> canvasList;
}
