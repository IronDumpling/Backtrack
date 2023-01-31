using UnityEngine;
using System.Collections;
using Backtrack.Core;

public class AccelerateRings : MonoBehaviour
{
    public const float accelerateTime = 5f;
    const string playerTag = "Player";

    private float accelerateTimeRemain;
    private bool accelerateActive = false;

    private void Start()
    {
        accelerateTimeRemain = accelerateTime;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // count timer
        if(accelerateActive && accelerateTimeRemain > 0)
        {
            accelerateTimeRemain -= Time.deltaTime;
        }
        // destroy
        else if(accelerateActive)
        {
            //ResetTimer();
            PlayerController.Instance.Decelerate();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(playerTag))
        {
            Debug.Log("Detect collision");
            PlayerController.Instance.Accelerate();
            accelerateActive = true;
            gameObject.SetActive(false);
        }
    }

    //private void ResetTimer()
    //{
    //    accelerateTimeRemain = accelerateTime;
    //    accelerateActive = false;
    //    gameObject.SetActive(true);
    //}
}

