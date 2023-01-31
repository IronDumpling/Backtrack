using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour
{
    const string playerTag = "Player";
    private int score;

    // Use this for initialization
    void Awake()
	{
        score = 100;
    }

	// Update is called once per frame
	void OnTriggerEnter(Collider col)
	{
        if (col.CompareTag(playerTag))
        {
            Collect();
        }
    }

    void Collect()
    {
        PointsManager.Instance.IncreasePoints(score);
        Destroy(gameObject);
    }
}

