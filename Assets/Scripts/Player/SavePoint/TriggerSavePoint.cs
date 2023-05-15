using System.Collections;
using System.Collections.Generic;
using Level3_Track;
#if UNITY_EDITOR
using UnityEditor.Build.Player;
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSavePoint : TriggerBase
{
    [SerializeField] private int levelNum = 0;
    protected override void enterEvent(Collider c)
    {
        if (levelNum == 3)
        {
            saveLevel3();
        }
        else
        {
            saveLevel0(c);
        }
    }

    private void saveLevel0(Collider c)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Vector3 point = c.transform.position;
        Vector3 rotation = c.transform.rotation.eulerAngles;
        string bgmName = AudioManager.Instance.GetMusicIsPlaying();
        float time = AudioManager.Instance.GetMusicTime(bgmName);
        int scores = ScoreManager.Instance.CurrentScoreInLevel;

        float speed = PlayerController.Instance.GetComponent<PlayerMotor>().ZSpeed;
        float xspeed = PlayerController.Instance.GetComponent<PlayerMotor>().XSpeed;

        SavePointManager.Instance.SetSavePoint(sceneName,
            point,
            rotation,
            bgmName,
            time,
            scores,
            speed,
            xspeed);
    }

    private void saveLevel3()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        string bgmName = AudioManager.Instance.GetMusicIsPlaying();
        float time = AudioManager.Instance.GetMusicTime(bgmName);
        int scores = ScoreManager.Instance.CurrentScoreInLevel;
        int trackId = TrackManager.Instance._CurrentTrackIdx;
        SavePointManager.Instance.SetSavePointLevel3(sceneName,
            bgmName,
            time,
            scores,
            trackId);
        
    }
}
