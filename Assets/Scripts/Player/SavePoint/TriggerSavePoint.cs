using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Build.Player;
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSavePoint : TriggerBase
{
    protected override void enterEvent(Collider c)
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
}
