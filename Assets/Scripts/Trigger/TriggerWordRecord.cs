using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;

public class TriggerWordRecord : TriggerBase
{
    public string word;
    private bool isTriggered = false;

    protected override void enterEvent()
    {
        base.enterEvent();

        isTriggered = true;

        string fileName = "level_1_p2_word.txt";

        string filePath = Path.Combine(Application.dataPath, "Resources/Text/Level1/" + fileName);

        File.WriteAllText(filePath, word);
    }
}

// 在每次开始时生产一个text file，记录本次游玩中玩家经过的字
// 如果玩家重新开始
// 如果玩家死亡后：1. 重新开始； 2. 从存档点开始

//public class HandleTextFile
//{
//    [MenuItem("Tool/Write file")]

//    static void WriteString()
//    {
//        string path = "Assets/Resources/Text/Level1";
//        StreamWriter writer = new StreamWriter(path, true);
//        writer.WriteLine("");
//        writer.Close();

//        AssetDatabase.ImportAsset(path);
//        TextAsset asset = Resources.Load("");
//        Debug.Log(asset.text);
//    }
//}