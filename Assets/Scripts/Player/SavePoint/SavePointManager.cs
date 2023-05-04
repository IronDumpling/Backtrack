using System.Collections;
using System.Collections.Generic;
using Common;
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
    }

    public void LoadSavePoint()
    {
        
        //如果没有则不load
        if (!isSave)
        {
            Debug.Log("没有存档");
            return;
        }
        GameObject asyncLoadObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MapObject/AsyncLevelObject"));
        asyncLoadObject.SetActive(true);
        asyncLoadObject.GetComponent<AsyncLevelLoader>().StartLoadAsync(saveSceneName);
        asyncLoadObject.GetComponent<AsyncLevelLoader>().onAfterFinishLoad += LoadAfterScene;

    }

    private void LoadAfterScene()
    {
        PlayerController.Instance.transform.position = savePointVector3;
        PlayerController.Instance.transform.rotation = Quaternion.Euler(saveRotationVector3);
        ScoreManager.Instance.CurrentScoreInLevel = saveScores;
        AudioManager.Instance.SetMusicTime(saveBGMName, saveBGMTime);
        AudioManager.Instance.Play(saveBGMName);
        
        PlayerController.Instance.GetComponent<PlayerMotor>().ZSpeed = speed;
        PlayerController.Instance.GetComponent<PlayerMotor>().XSpeed = xspeed;
        
    }
}
