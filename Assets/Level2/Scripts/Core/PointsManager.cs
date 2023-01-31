using UnityEngine;
using System.Collections;
using Backtrack.Core;
using System;

public class PointsManager : MonoBehaviour
{
    // Returns the PointsManager.
    public static PointsManager Instance => s_Instance;
    static PointsManager s_Instance;

    [SerializeField] int m_Points = 0;

    // Use this for initialization
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

    public void IncreasePoints(int score)
    {
        m_Points += score;
    }

    public void DecreasePoints(int score)
    {
        m_Points = Math.Max(m_Points - score, 0);
    }

    public void ResetPoints()
    {
        m_Points = 0;
    }

    private void OnGUI()
    {
        var style = new GUIStyle();
        style.fontSize = 30;
        GUI.Label(new Rect(20, 20, 400, 100), "Score: " + m_Points, style);
    }
}

