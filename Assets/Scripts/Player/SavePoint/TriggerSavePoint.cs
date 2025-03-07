using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
        float timeScale = Time.timeScale;

        int curNum = CameraManager.Instance.GetCurCameraIdx();
        if(curNum == -1) DebugLogger.Error(this.name, "cant find camera in savelevel.");
        
        SavePointManager.Instance.SetSavePoint(sceneName,
            point,
            rotation,
            bgmName,
            time,
            scores,
            speed,
            xspeed,
            curNum,
            timeScale);
    }

    private void saveLevel3()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        string bgmName = AudioManager.Instance.GetMusicIsPlaying();
        float time = AudioManager.Instance.GetMusicTime(bgmName);
        int scores = ScoreManager.Instance.CurrentScoreInLevel;
        int trackId = TrackManager.Instance._CurrentTrackIdx;
        float trackPosition = TrackManager.Instance._playerDollyCart.m_Position;
        SavePointManager.Instance.SetSavePointLevel3(sceneName,
            bgmName,
            time,
            scores,
            trackId,
            trackPosition);
        
    }
}
