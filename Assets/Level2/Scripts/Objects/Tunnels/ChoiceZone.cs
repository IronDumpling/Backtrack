using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Backtrack.Core;

public class ChoiceZone : MonoBehaviour
{
	[SerializeField] GameObject leftBranch;
    [SerializeField] GameObject rightBranch;
    Collider m_Collider;
    Vector3 m_Center;
    const string playerTag = "Player";

    // Use this for initialization
    void Start()
	{
		m_Collider = gameObject.GetComponent<Collider>();
		m_Center = m_Collider.bounds.center;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
			// get collide point position
			Vector3 position = this.transform.position;
			Vector3 closestPoint = col.ClosestPoint(position);

            PlayerController.Instance.isRotating = true;
            PlayerController.Instance.rotateFrom = col.gameObject.transform;

            if (closestPoint.x < m_Center.x)
			{
                PlayerController.Instance.rotateTo = leftBranch.transform;
			}
			else
			{
                PlayerController.Instance.rotateTo = rightBranch.transform;
            }
        }
    }
}

