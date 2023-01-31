using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// A class used to store game state information, 
// load levels, and save/load statistics as applicable.
// The GameManager class manages all game-related 
// state changes.
namespace Backtrack.Core
{
    public class GameManager : MonoBehaviour
    {
        // Returns the GameManager.
        public static GameManager Instance => s_Instance;
        static GameManager s_Instance;

        [SerializeField]
        AbstractGameEvent m_WinEvent;

        [SerializeField]
        AbstractGameEvent m_LoseEvent;

        //LevelDefinition m_CurrentLevel;

        // Returns true if the game is currently active.
        // Returns false if the game is paused, has not yet begun, or has ended.
        public bool IsPlaying => m_IsPlaying;
        bool m_IsPlaying;
        GameObject m_CurrentLevelGO;
        GameObject m_LevelMarkersGO;

        List<Spawnable> m_ActiveSpawnables = new List<Spawnable>();

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
            DontDestroyOnLoad(this);
        }

        // This method calls all methods necessary to restart a level,
        // including resetting the player to their starting position
        public void ResetLevel()
        {
            //PlayerController.Instance.ResetPlayer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //if (LevelManager.Instance != null)
            //{
            //    LevelManager.Instance.ResetSpawnables();
            //}
        }

        public void UnloadCurrentLevel()
        {
            if (m_CurrentLevelGO != null)
            {
                GameObject.Destroy(m_CurrentLevelGO);
            }

            if (m_LevelMarkersGO != null)
            {
                GameObject.Destroy(m_LevelMarkersGO);
            }
        }

        void StartGame()
        {
            ResetLevel();
            m_IsPlaying = true;
        }

        public void Win()
        {
            m_WinEvent.Raise();
            ResetLevel();
            PointsManager.Instance.ResetPoints();
        }

        public void Lose()
        {
            const int score = 200;
            m_LoseEvent.Raise();
            PointsManager.Instance.DecreasePoints(score);
            ResetLevel();
        }
    }

}



