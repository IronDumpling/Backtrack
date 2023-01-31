using System.Collections;
using System.Collections.Generic;
using Backtrack.Core;
using UnityEditor;
using UnityEngine;

// A class used to control a player in a Runner
// game. Includes logic for player movement as well as 
// other gameplay logic.
public class PlayerController : MonoBehaviour
{
    // Returns the PlayerController
    public static PlayerController Instance => s_Instance;
    static PlayerController s_Instance;

    enum PlayerSpeedMode
    {
        Slow,
        Medium,
        Fast
    }

    [SerializeField]
    PlayerSpeedMode m_PlayerSpeedMode = PlayerSpeedMode.Slow;

    float[] m_PlayerSpeed = new float[]
    {
        15.0f, // slow
        20.0f, // mid
        27.0f, // fast
    };

    float m_Speed;
    Transform m_Transform;
    private Rigidbody m_Rigid;
    Vector3 m_StartPosition;
    Quaternion m_StartRotation;

    public bool isRotating = false;
    public Transform rotateFrom;
    public Transform rotateTo;
    float _timeCount = 0.0f;

    void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;

        Initialize();
    }

    // Set up all necessary values for the PlayerController.
    public void Initialize()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_StartPosition = m_Transform.position;
        m_StartRotation = m_Transform.rotation;
        m_Rigid = gameObject.GetComponent<Rigidbody>();

        SetSpeed();
    }

    private void Update()
    {
        if (isRotating && _timeCount < 1.0f)
        {
            SmoothRotation(rotateFrom, rotateTo, _timeCount);
            _timeCount += Time.deltaTime;
        }
        else if (isRotating)
        {
            _timeCount = 0.0f;
            isRotating = false;
        }
    }

    // Set the player's speed based on speed mode
    private void SetSpeed()
    {
        m_Speed = m_PlayerSpeed[(int)m_PlayerSpeedMode];
        Debug.Log("Current Speed: " + m_Speed + " Current Speed Mode: " + m_PlayerSpeedMode);
    }

    public void Move(Vector2 playerInput)
    {
        m_Rigid.velocity = new Vector3(2 * playerInput.x, 2 * playerInput.y, m_Speed);
        m_Rigid.velocity = m_Transform.rotation * m_Rigid.velocity;
    }

    public void Accelerate()
    {
        if (m_PlayerSpeedMode == PlayerSpeedMode.Slow)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Medium;
        }
        else if(m_PlayerSpeedMode == PlayerSpeedMode.Medium)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Fast;
        }

        SetSpeed();
    }

    public void Decelerate()
    {
        if (m_PlayerSpeedMode == PlayerSpeedMode.Medium)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Slow;
        }
        else if (m_PlayerSpeedMode == PlayerSpeedMode.Fast)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Medium;
        }
        SetSpeed();
    }

    public void SmoothRotation(Transform from, Transform to, float time)
    {
        m_Transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, time);
    }
}
