using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoData", menuName = "SO/Data")]
public class PlayerData_SO : ScriptableObject
{

    [Header("Level Data")] 
    public int level0Score;
    public string level0Choices;
    public int level3Score;
}
