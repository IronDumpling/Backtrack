using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoUI : MonoBehaviour
{
    private GameObject _collection;
    private GameObject _title;
    private GameObject _description;

    [SerializeField] private List<string> _levelTitles;
    [SerializeField] private List<string> _levelDescriptions;

    // Start is called before the first frame update
    void Awake()
    {
        _title = transform.Find("LevelTitle")?.gameObject;
        _collection = transform.Find("CollectionUI")?.gameObject;
        _description = transform.Find("Description")?.gameObject;
    }

    // Update is called once per frame
    public void HoverLevel(int levelNum)
    {
        _title.GetComponent<TMPro.TMP_Text>().text = $"{_levelTitles[levelNum]}";
        _description.GetComponent<TMPro.TMP_Text>().text = $"{_levelDescriptions[levelNum]}";
        _collection.GetComponent<CollectionUI>().ShowScore(levelNum);
    }
}
