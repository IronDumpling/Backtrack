using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    private GameObject _specialMain;
    private GameObject _normMain;
    private Transform _background;
    private PlayerData_SO _playerData;

    // Start is called before the first frame update
    void Awake()
    {
        _specialMain = transform.Find("SpecialUI")?.gameObject;
        _normMain = transform.Find("NormalUI")?.gameObject;
        _background = _normMain.transform.Find("Background");
        _playerData = Resources.Load<PlayerData_SO>("GameData/PlayerData");
    }

    // Update is called once per frame
    void Start()
    {
        if (_playerData.level4Score != 0) CompleteLevel4();
        else if (_playerData.level3Score != 0) CompleteLevel3();
        else if (_playerData.level0Score != 0) CompleteLevel0();
        else NewGame();
    }

    void NewGame()
    {
        _specialMain.SetActive(true);
        _normMain.SetActive(false);
    }

    void CompleteLevel0()
    {
        _specialMain.SetActive(false);
        _normMain.SetActive(true);
        _background.Find("Level1Background").gameObject.SetActive(true);
        _background.Find("Level2Background").gameObject.SetActive(false);
        _background.Find("Level3Background").gameObject.SetActive(false);
    }

    void CompleteLevel3()
    {
        _specialMain.SetActive(false);
        _normMain.SetActive(true);
        _background.Find("Level1Background").gameObject.SetActive(false);
        _background.Find("Level2Background").gameObject.SetActive(true);
        _background.Find("Level3Background").gameObject.SetActive(false);
    }

    void CompleteLevel4()
    {
        _specialMain.SetActive(false);
        _normMain.SetActive(true);
        _background.Find("Level1Background").gameObject.SetActive(false);
        _background.Find("Level2Background").gameObject.SetActive(false);
        _background.Find("Level3Background").gameObject.SetActive(true);
    }
}
