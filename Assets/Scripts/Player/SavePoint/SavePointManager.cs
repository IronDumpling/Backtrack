using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Level3_Track;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePointManager : Singleton<SavePointManager>
{
    public bool isSave = false;
    public string saveSceneName;
    public Vector3 savePointVector3;
    public Vector3 saveRotationVector3;
    public string saveBGMName;
    public float saveBGMTime;
    public int saveScores;

    public float speed;
    public float xspeed;

    public int levelNum;
    public int saveTrackId;
    
    
    public void SetSavePoint(string sceneName, Vector3 pointVector,Vector3 rotation,  string bgmName, float bgmTime,int scores,
        float speed, float xspeed)
    {
        isSave = true;
        
        saveSceneName = sceneName;
        savePointVector3 = pointVector;
        saveRotationVector3 = rotation;
        saveBGMName = bgmName;
        saveBGMTime = bgmTime;
        saveScores = scores;
        this.speed = speed;
        this.xspeed = xspeed;
        // TODO: Add Camaera Info
        // TODO: Add choice info
        levelNum = 0;
    }

    public void SetSavePointLevel3(string sceneName, string bgmName, float bgmTime,int scores, 
        int trackId)
    {
        isSave = true;
        levelNum = 3;
        saveSceneName = sceneName;
        saveTrackId = trackId;
        saveBGMName = bgmName;
        saveBGMTime = bgmTime;
        saveScores = scores;
        
        Debug.Log("set level3 save success");
        


    }
    public void LoadSavePoint()
    {
        
        if (!isSave)
        {
            Debug.Log("没有存档");
            GameObject a2 = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MapObject/AsyncLevelObject"));
            a2.SetActive(true);
            a2.GetComponent<AsyncLevelLoader>().StartLoadAsync(SceneManager.GetActiveScene().name);
            return;
        }

     
        
        
        GameObject asyncLoadObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MapObject/AsyncLevelObject"));
        asyncLoadObject.SetActive(true);
        asyncLoadObject.GetComponent<AsyncLevelLoader>().StartLoadAsync(saveSceneName);
        asyncLoadObject.GetComponent<AsyncLevelLoader>().onAfterFinishLoad += LoadAfterScene;

    }

    private void LoadAfterScene()
    {
        if (levelNum == 3)
        {
            TrackManager.Instance._CurrentTrackIdx = saveTrackId;
            TrackManager.Instance.TrackSwitch();

            
        }
        else
        {
            PlayerController.Instance.transform.position = savePointVector3;
            PlayerController.Instance.transform.rotation = Quaternion.Euler(saveRotationVector3);
            PlayerController.Instance.GetComponent<PlayerMotor>().ZSpeed = speed;
            PlayerController.Instance.GetComponent<PlayerMotor>().XSpeed = xspeed;


        }
        ScoreManager.Instance.CurrentScoreInLevel = saveScores;
        AudioManager.Instance.SetMusicTime(saveBGMName, saveBGMTime);
        AudioManager.Instance.Play(saveBGMName);
        

    }
}
