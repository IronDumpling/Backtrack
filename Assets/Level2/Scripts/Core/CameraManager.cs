using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Backtrack.Core;

public class CameraManager : MonoBehaviour
{
    // Returns the CameraManager.
    public static CameraManager Instance => s_Instance;
    static CameraManager s_Instance;

    enum CameraAnglePreset
    {
        Tilt,
        Overhead,
        Side,
        Behind,
    }

    Vector3[] m_PresetOffsets = new Vector3[]
    {
            new Vector3(0.8f, 0.05f, -2f), // Tilt
            new Vector3(0.0f, 1.0f, 0.0f), // Overhead
            new Vector3(5.0f, 5.0f, -5.0f), // Side
            new Vector3(0.0f, 1.0f, -5.0f) // Behind
    };

    Vector3[] m_PresetLookAtOffsets = new Vector3[]
    {
            new Vector3(-1f, 0f, 1.5f), // Tilt 
            new Vector3(0.0f, 0.0f, 4.0f), // Overhead
            new Vector3(-0.5f, 1.0f, 2.0f), // Side
            new Vector3(0.0f, 2.0f, 6.0f) // Behind
    };

    [SerializeField]
    CameraAnglePreset m_CameraAnglePreset = CameraAnglePreset.Tilt;

    [SerializeField]
    bool m_SmoothCameraFollow;

    [SerializeField]
    float m_SmoothCameraFollowStrength = 10.0f;

    Transform m_Transform;

    // Use this for initialization
    void Awake()
	{
        SetupInstance();
    }

    void OnEnable()
    {
        SetupInstance();
    }

    void SetupInstance()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;
        m_Transform = gameObject.GetComponent<Transform>();
    }

    Vector3 GetPlayerPosition()
    {
        Vector3 playerPosition = Vector3.up;

        if (PlayerController.Instance != null)
        {
            playerPosition = PlayerController.Instance.transform.position;
        }

        return playerPosition;
    }

    // Update is called once per frame
    void LateUpdate()
	{
        if (m_Transform == null)
        {
            return;
        }

        SetCameraPositionAndOrientation(m_SmoothCameraFollow);
    }

    Vector3 GetCameraOffset()
    {
        return m_PresetOffsets[(int)m_CameraAnglePreset];
    }

    Vector3 GetCameraLookAtOffset()
    {
        return m_PresetLookAtOffsets[(int)m_CameraAnglePreset];
    }

    void SetCameraPositionAndOrientation(bool smoothCameraFollow)
    {
        Vector3 playerPosition = GetPlayerPosition();

        Vector3 positionOffset = playerPosition + GetCameraOffset();
        Vector3 lookAtOffset = playerPosition + GetCameraLookAtOffset();

        if (smoothCameraFollow)
        {
            float lerpAmound = Time.deltaTime * m_SmoothCameraFollowStrength;

            m_Transform.position = Vector3.Lerp(m_Transform.position, positionOffset, lerpAmound);
            m_Transform.LookAt(Vector3.Lerp(m_Transform.position + m_Transform.forward, lookAtOffset, lerpAmound));

            m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, positionOffset.z);
        }
        else
        {
            m_Transform.position = playerPosition + GetCameraOffset();
            m_Transform.LookAt(lookAtOffset);
        }
    }

    public void ResetCamera()
    {
        SetCameraPositionAndOrientation(false);
    }
}

