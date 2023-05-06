using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WordCombineUI : MonoBehaviour
{
    public TextAsset displayText;

    private void Start()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Text/Level1/level_1_p2_word");

        string allText = "";

        foreach (TextAsset textAsset in textAssets)
        {
            allText += textAsset.text + "\n";
        }

        
    }


}
