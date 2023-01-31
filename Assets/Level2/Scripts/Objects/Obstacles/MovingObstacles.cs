using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : Obstacles
{
    public enum MovingPlatformType
    {
        BACK_FORTH,
        LOOP
    }

    public float speed = 1.0f;
    public MovingPlatformType platformType;

    public bool startMovingOnlyWhenVisible;
    public bool isMovingAtStart = true;

    [HideInInspector]
    public Vector3[] localNodes = new Vector3[1];

    public float[] waitTimes = new float[1];

    public Vector3[] worldNode { get { return m_WorldNode; } }

    protected Vector3[] m_WorldNode;

    protected int m_Current = 0;
    protected int m_Next = 0;
    protected int m_Dir = 1;

    protected float m_WaitTime = -1.0f;

    protected Rigidbody m_Rigidbody;
    protected Vector3 m_Velocity;

    protected bool m_Started = false;
    protected bool m_VeryFirstStart = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;

        Initiate();
    }

    protected void Initiate()
    {
        m_Current = 0;
        m_Dir = 1;
        m_Next = localNodes.Length > 1 ? 1 : 0;

        m_WaitTime = waitTimes[0];

        m_VeryFirstStart = false;
        if (isMovingAtStart)
        {
            m_Started = !startMovingOnlyWhenVisible;
            m_VeryFirstStart = true;
        }
        else
            m_Started = false;
    }

    // Update is called once per frame
    void FixUpdate()
    {
        if (!m_Started)
            return;

        //no need to update we have a single node in the path
        if (m_Current == m_Next)
            return;

        if (m_WaitTime > 0)
        {
            m_WaitTime -= Time.deltaTime;
            return;
        }

        float distanceToGo = speed * Time.deltaTime;

        while (distanceToGo > 0)
        {
            Vector2 direction = m_WorldNode[m_Next] - transform.position;

            float dist = distanceToGo;
            if (direction.sqrMagnitude < dist * dist)
            {   //we have to go farther than our current goal point, so we set the distance to the remaining distance
                //then we change the current & next indexes
                dist = direction.magnitude;

                m_Current = m_Next;

                m_WaitTime = waitTimes[m_Current];

                if (m_Dir > 0)
                {
                    m_Next += 1;
                    if (m_Next >= m_WorldNode.Length)
                    {   //we reach the end
                        switch (platformType)
                        {
                            case MovingPlatformType.BACK_FORTH:
                                m_Next = m_WorldNode.Length - 2;
                                m_Dir = -1;
                                break;
                            case MovingPlatformType.LOOP:
                                m_Next = 0;
                                break;
                        }
                    }
                }
                else
                {
                    m_Next -= 1;
                    if (m_Next < 0)
                    {   //reached the beginning again
                        switch (platformType)
                        {
                            case MovingPlatformType.BACK_FORTH:
                                m_Next = 1;
                                m_Dir = 1;
                                break;
                            case MovingPlatformType.LOOP:
                                m_Next = m_WorldNode.Length - 1;
                                break;
                        }
                    }
                }
            }

            m_Velocity = direction.normalized * dist;

            //transform.position +=  direction.normalized * dist;
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Velocity);
            MoveObstacle(m_Velocity);
            //We remove the distance we moved. That way if we didn't had enough distance to the next goal, we will do a new loop to finish
            //the remaining distance we have to cover this frame toward the new goal
            distanceToGo -= dist;

            // we have some wait time set, that mean we reach a point where we have to wait. So no need to continue to move the platform, early exit.
            if (m_WaitTime > 0.001f)
                break;
        }
    }

    public void StartMoving()
    {
        m_Started = true;
    }

    public void StopMoving()
    {
        m_Started = false;
    }

    public void MoveObstacle(Vector3 velocity)
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + velocity);
    }
}
